app.service('userService', function($http){
	var url = '/api/user';
	this.register = function(user){
		return $http.post(url + "/register", user);
	}

	this.login = function(user){
		return $http.post("api/login", user);
	}

  this.getLoggedUser = function(){
		return $http.get("api/login");
	}
});
