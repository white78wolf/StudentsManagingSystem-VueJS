import Vue from '../vendor/vue';
import axios from '../vendor/axios.min.js';

Vue.config.devtools = true;

var app = new Vue({
    el: "#app",
    data() {
        return {
            form: {                
                id: 0,
                uniqId:     "",
                name:       "",
                middleName: "",
                lastName:   "",
                gender:     ""
            },
            studentsList: [],
            errors: [],
            loading: true,
            errored: false,            
            filteredOutput: "всего"            
        }
    },

    computed: {
        numberOfStudents: function () {
            return this.studentsList.length;
        }
    },

    methods: {
        getStudents: function () {
            axios
                .get('api/students')
                .then(response => this.studentsList = response.data)
                .catch(error => {
                    console.log(error);
                    this.errored = true;
                })
                .finally(() => (this.loading = false));

            this.filteredOutput = "всего";
        },
        // fetching selected student's data to edit in form
        getStudent: function (id) {            
            axios
                .get('api/students/' + id)
                .then(response => {
                    this.form.id = response.data.id,
                        this.form.uniqId = response.data.uniqId == null ? "" : response.data.uniqId, // it can be null because of table's field property
                        this.form.name = response.data.name,
                        this.form.middleName = response.data.middleName == null ? "" : response.data.middleName, // the same
                        this.form.lastName = response.data.lastName,
                        this.form.gender = response.data.gender
                })
                .catch(e => {
                    this.errors.push(e.response.data[""].toString());
                });            
        },      
        // common function for both filtering and sorting of incoming data
        filterAndSorting: function (param) {
            axios
                .get('api/filter/' + param)
                .then(response => {
                    this.studentsList = response.data;
                })
                .catch(error => {
                    console.log(error);
                    this.errored = true;
                })
                .finally(() => (this.loading = false));
        },

        createStudent: function () {
            axios
                .post('api/students/', {
                    uniqId:     this.form.uniqId,
                    name:       this.form.name,
                    middleName: this.form.middleName,
                    lastName:   this.form.lastName,
                    gender:     this.form.gender
                })
                .then(response => this.studentsList.push(response.data))
                .catch(e => {
                    this.errors.push(e.response.data[""].toString());
                });
        },

        editStudent: function () {
            axios.
                put('api/update/', {
                    id:         this.form.id,
                    uniqId:     this.form.uniqId,
                    name:       this.form.name,
                    middleName: this.form.middleName,
                    lastName:   this.form.lastName,
                    gender:     this.form.gender
                })
                .then(response => this.changeLine(response.data))
                .catch(e => {
                    this.errors.push(e.response.data[""].toString());
                });
        },
        // function to update line in view's table by reactive updating selected object in array of students
        changeLine: function (resData) {
            this.$set(this.studentsList,
                this.studentsList.indexOf(this.studentsList.find(x => x.id === resData.id)),
                resData);
        },
        // client's validation
        validFields: function () {
            if (!this.form.uniqId.trim() == "") {
                if (this.form.uniqId.length < 6 || this.form.uniqId.length > 16)
                    this.errors.push("Поле 'Уникальный идетификатор' может быть пустым либо должно содержать от 6 до 16 символов");
            }

            if (this.form.name.trim() == "") {
                this.errors.push("Укажите имя");
            }

            if (this.form.lastName.trim() == "") {
                this.errors.push("Укажите фамилию");
            }

            if (this.form.gender == "") {
                this.errors.push("Укажите пол");
            }

            if (!this.errors.length)
                return true;
        },
        // submiting form
        submitFunc: function () {
            this.errors = [];

            if (this.validFields()) {
                if (this.form.id === 0) {
                    this.createStudent();
                }
                else
                    this.editStudent();
                this.reset();
            }
        },

        deleteStudent: function (id) {
            // getting an index of a record which will be deleted
            var idxOfRecord = this.studentsList.indexOf(this.studentsList.find(student => student.id === id));
            axios
                .delete('api/delete/' + id)
                .then(response => this.studentsList.splice(idxOfRecord, 1))
                .catch(e => {
                    this.errors.push(e.response.data[""].toString());
                });
            this.reset();
        },

        reset: function () {
            this.form.id = 0,
                this.form.uniqId     = "",
                this.form.name       = "",
                this.form.middleName = "",
                this.form.lastName   = "",
                this.form.gender     = "",
                this.errors = [];
        }
    },
    // one of lifecycle's hooks - when app is mounting to index.html - to query all students' list
    mounted() {
        this.getStudents();
    }
});