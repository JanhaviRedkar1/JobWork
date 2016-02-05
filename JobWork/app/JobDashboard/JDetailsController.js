(function () {

    var app = angular.module('app');
    app.controller('JDetailsController', function ($http, $routeParams) {
        var jdctrl = this;
        var jobs = null;

        $http({
            method: 'GET',
            url: "api/Job_Board_Table/" + $routeParams.Job_Id
        }).success(function (data) {
            jdctrl.jobs = data;
            console.log(data);
        }).error(function (error) {
            console.log(error);
        });

    });
})();