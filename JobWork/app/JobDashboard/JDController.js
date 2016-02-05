(function () {

    var app = angular.module('app');
    app.controller('JDController', function ($http) {
        var jctrl = this;
        var jobs = null;
        
            $http({
                method: 'GET',
                url: "api/Jobs"
            }).success(function (data) {
                jctrl.job = data.JobData;
                console.log(jctrl.job);
            }).error(function (error) {
                console.log(error);
            });
            jctrl.getJob = function () {

                if (jctrl.Job_Id) {
                    $http({
                        method: 'GET',
                        url: "api/Jobs" + jctrl.Job_Id
                    }).success(function (data) {
                        jctrl.Job = data.JobData;
                        console.log(data);
                    }).error(function (error) {
                        console.log(error);
                    });
                }
            };

            jctrl.addJobs = function () {

                console.log(jctrl.newjob);
                $http({
                    method: 'POST',
                    url: "api/Job_Board_Table",
                    data: JSON.stringify(jctrl.newjob),
                    headers: { 'Content-Type': 'application/JSON' }
                }).success(function (data)
                {
                    //Showing success message
                    jctrl.status = "The Job Saved Successfully!!!";
                    //Updating Jobs Model
                   

                    console.log(data);
                    jctrl.jobs = data.payload;
                    jctrl.newjob = null;
                }).error(function (error) {
                    //Showing error message
                    jctrl.status = 'Unable to create a Job: ' + error.message;
                });

            };
            jctrl.editJob = function (JobId) {
                for (i in jctrl.Jobs) {
                    //Getting the Job details from scope based on id
                    if (jctrl.Jobs[i].Id == JobId) {
                        //Assigning the Create user scope variable for editing
                        jctrl.newJob = {
                            Job_Id: jctrl.Jobs[i].Job_Id,
                           Job_Title: jctrl.Jobs[i].Job_Title,
                            Job_Desc: jctrl.Jobs[i].Job_Desc,
                            Job_Skil: jctrl.Jobs[i].Job_Skill,
                            Job_Resp: jctrl.Jobs[i].Job_Resp,
                            Job_Start_Date: jctrl.Jobs[i].Job_Start_Date,
                            Job_End_Date: jctrl.Jobs[i].Job_End_Date,
                            Job_Creadted_By: jctrl.Jobs[i].Job_Creadted_By,
                            Job_Created_Date: jctrl.Jobs[i].Job_Created_Date,
                            Job_Updated_By: jctrl.Jobs[i].Job_Updated_By,
                            Job_Updated_Date: jctrl.Jobs[i].Job_Updated_Date,
                            Job_Status: jctrl.Jobs[i].Job_Status
                            
                        };
                        
                        jctrl.status = '';
                    }
                }
            }

            jctrl.updateJob = function () {

                console.log(jctrl.newjob);
                $http({
                    method: 'PUT',
                    url: "api/Job_Board_Table",
                    data: jctrl.newjob
                }).success(function (data) {
                    console.log(data);
                    jctrl.jobs = data.payload;
                    jctrl.newjob = null;
                }).error(function (error) {
                    console.log(error);
                });
            };



            jctrl.deleteJob = function () {

                if (jctrl.Job_Id) {
                    $http({
                        method: 'DELETE',
                        url: 'api/Job_Board_Table/' + jctrl.Job_Id

                        //headers: {"Content-Type": "application/json;charset=utf-8"}
                    }).success(function (data) {
                        jctrl.jobs = data.payload;
                        //location.reload();
                    }).error(function (data) {
                        console.log(data);
                    });
                }


            };




       
    });
})();