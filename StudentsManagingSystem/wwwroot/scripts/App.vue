<template>
    <div class="container">
            <div class="row">
                <h2>Список студентов <small>({{filteredOutput}}: {{numberOfStudents}} чел.)</small></h2> <!-- Showing number of students -->
                <div class="alert alert-danger" v-if="errors.length">
                    <ul>
                        <li v-for="error in errors">{{error}}</li> <!-- Showing errors of form fields validation -->
                    </ul>
                </div>

                <div class="col-md-3"> <!-- Left side of page - the form -->
                    <form name="studentForm">
                        <input type="hidden" v-model="form.id" />
                        <div class="form-group">
                            <label for="uniqid">Уникальный идентификатор:</label>
                            <input class="form-control" v-model="form.uniqId" type="text" placeholder="от 6 до 16 знаков или пустое поле" />
                        </div>
                        <div class="form-group">
                            <label for="name">Имя:</label>
                            <input class="form-control" v-model="form.name" type="text" placeholder="обязательное поле" />
                        </div>
                        <div class="form-group">
                            <label for="middlename">Отчество:</label>
                            <input class="form-control" v-model="form.middleName" type="text" />
                        </div>
                        <div class="form-group">
                            <label for="lastname">Фамилия:</label>
                            <input class="form-control" v-model="form.lastName" type="text" placeholder="обязательное поле" />
                        </div>
                        <div class="form-group" data-toggle="buttons">
                            <label for="gender">Пол (обяз.):</label>
                            муж. <input type="radio" value="0"   v-model="form.gender" />
                            жен. <input type="radio" value="1"   v-model="form.gender" />
                        </div>
                        <div class="panel-body">
                            <button type="button" class="btn btn-sm btn-primary" v-on:click="submitFunc">Сохранить</button>
                            <button type="button" class="btn btn-sm btn-primary" v-on:click="reset"     >Сбросить </button>
                        </div>
                    </form>                    
                </div>

                <div class="col-md-9"> <!-- Right side of page - the list of students -->
                    <h4>Пример фильтра: </h4>
                    <div class="btn-group" role="group" aria-label="..."> <!-- The example of filtering list by gender -->
                        <button type="button" class="btn btn-default" v-on:click="getStudents" >Все </button>
                        <button type="button" class="btn btn-default" v-on:click="filtering(0)">Муж.</button>
                        <button type="button" class="btn btn-default" v-on:click="filtering(1)">Жен.</button>
                    </div>

                    <section id="error-message" v-if="errored"> <!-- Showing errors of downloading students list -->
                        <p>Запрос вернул ошибку</p>
                    </section>

                    <section v-else>
                        <div v-if="loading"><i class="icon-spinner animate-spin"></i> Loading... </div> <!-- Displaying the process of loading data -->
                    </section>

                    <table class="table table-condensed table-striped table-bordered">
                        <thead>
                            <tr>                               
                                <th> Уник. ID </th>
                                <th><a v-on:click="sorting('name')"> Имя </a></th>
                                <th> Отчество </th>
                                <th><a v-on:click="sorting('lastname')"> Фамилия </a></th>
                                <th> Пол </th>
                                <th> Действия </th>
                            </tr>
                        </thead>

                        <tbody>
                            <tr v-for="student in studentsList" v-bind:key="student.id">
                                <td>{{student.uniqId}}    </td>
                                <td>{{student.name}}      </td>
                                <td>{{student.middleName}}</td>
                                <td>{{student.lastName}}  </td>
                                <td v-if  ="student.gender==0"> муж. </td>
                                <td v-else="student.gender==1"> жен. </td>
                                <td>
                                    <button type="button">
                                        <i class="icon-pencil" v-on:click="getStudent(student.id)" title="Редактировать"></i>
                                    </button>
                                    <button type="button">
                                        <i class="icon-user-times" v-on:click="deleteStudent(student.id)" title="Удалить"></i>
                                    </button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>    
</template>

<script>
    import axios from '../vendor/axios.min.js'
    
    export default {
        name: 'App',
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
                filteredOutput: "всего", 
                sortingOrder: "lastname"
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
                        this.form.gender = String(response.data.gender)
                    })
                    .catch(e => {
                        this.errors.push(e.response.data[""].toString());
                    });            
            },      
            // common function for both filtering and sorting of incoming data
            filtering: function (param) {
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

            sorting: function (param) {
                param = param == this.sortingOrder ? param + "_desc" : param;
                this.sortingOrder = param;

                axios
                    .get('api/sort/' + param)
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
                        gender:     parseInt(this.form.gender)
                    })
                    .then(response => this.studentsList.push(response.data))
                    .catch(e => {
                        this.errors.push(e.response.data[""].toString());
                    });            
            },

            editStudent: function () {
                axios.
                    put('api/students/', {
                        id:         this.form.id,
                        uniqId:     this.form.uniqId,
                        name:       this.form.name,
                        middleName: this.form.middleName,
                        lastName:   this.form.lastName,
                        gender:     parseInt(this.form.gender)
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
                    .delete('api/students/' + id)
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
    }    
</script>

<style>
    
</style>