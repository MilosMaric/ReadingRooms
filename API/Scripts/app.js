var app = angular.module('app', ['ngRoute', 'ui.bootstrap']);

app.config(['$routeProvider', '$httpProvider', function ($routeProvider, $httpProvider) {
    $routeProvider
			.when('/login', {
			    controller: 'loginController',
			    templateUrl: 'Scripts/partials/loginPartial.html',
			})
      .when('/register', {
			    controller: 'registerController',
			    templateUrl: 'Scripts/partials/registerPartial.html',
			})
      .when('/profile', {
			    controller: 'profileController',
			    templateUrl: 'Scripts/partials/profilePartial.html',
			})
      .when('/faculty', {
			    controller: 'facultyController',
			    templateUrl: 'Scripts/partials/facultyPartial.html',
			})
      .when('/uni', {
			    controller: 'uniController',
			    templateUrl: 'Scripts/partials/uniPartial.html',
			})
      .when('/users', {
			    controller: 'usersController',
			    templateUrl: 'Scripts/partials/usersPartial.html',
			})
      .when('/rroom', {
			    controller: 'rroomsController',
			    templateUrl: 'Scripts/partials/rroomsPartial.html',
			})
	    .otherwise({
	        redirectTo: '/'
	    });
      $httpProvider.interceptors.push('httpRequestInterceptor');
}]);

//presretač zahteva http servisa i ubacivanje JWT u headere
app.factory('httpRequestInterceptor', ['$window', function ($window) {
  return {
    request: function (config) {
      var token = $window.localStorage.token;
      if(token){
         config.headers['Authorization'] = token;
      }
      return config;
    }
  };
}]);
