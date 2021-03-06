﻿(function () {
    'use strict';

    var app = angular.module('app');

    // Collect the routes
    app.constant('routes', getRoutes());
    
    // Configure the routes and route resolvers
    app.config(['$routeProvider', 'routes', routeConfigurator]);
    function routeConfigurator($routeProvider, routes) {

        routes.forEach(function (r) {
            $routeProvider.when(r.url, r.config);
        });
        $routeProvider.otherwise({ redirectTo: '/' });
    }

    // Define the routes 
    function getRoutes() {
        return [
            {
                url: '/',
                config: {
                    templateUrl: 'app/JobDashboard/JobDashboad.html',
                    title: 'Jobdashboard',
                    settings: {
                        nav: 1,
                        content: '<i class="fa fa-dashboard"></i> Jobs Dashboard'
                    }
                }
            }, {
                url: '/NewJob',
                config: {
                    title: 'Create New Job',
                    templateUrl: 'app/JobDashboard/NewJob.html',
                    settings: {
                        nav: 2,
                        content: '<i class="fa fa-lock"></i> New Job'
                    }
                }
            }
        ];
    }
})();