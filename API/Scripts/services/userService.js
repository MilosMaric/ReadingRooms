app.service('userService', function($http){
	var url = '/api/user';

	this.login = function(user){
		return $http.post("api/login", user);
	}

  this.getLoggedUser = function(){
		return $http.get("api/login");
	}

	this.getStudentReservations = function(id) {
		return $http.get(url + "/" + id + "/reservations");
	}

	this.getAll = function() {
		return $http.get(url);
	}

	this.getById = function(id) {
		return $http.get(url + "/" + id);
	}

	this.add = function(user){
		return $http.post(url, user);
	}

  this.update = function(id, user){
		return $http.put(url + "/" + id, user);
	}

  this.delete = function(id){
		return $http.delete(url + "/" + id);
	}
});
