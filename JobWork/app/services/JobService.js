(function () {
    var app = angular.module('app');
    app.factory('JobService', ['$http', function ($http) {

        var urlBase = 'http://localhost:61731/api';
        var JobService = {};
        JobService.getJobs = function () {
            return $http.get(urlBase + '/');
        };

        return JobService;
    }]);
})();