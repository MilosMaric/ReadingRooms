app.service('facultyService', function($http){
	var url = '/api/faculty';

	this.getAll = function(user){
		return $http.get(url);
	}

	this.add = function(fac){
		return $http.post(url, fac);
	}

	this.update = function(id, fac){
		return $http.put(url + "/" + id, fac);
	}

	this.delete = function(id){
		return $http.delete(url + "/" + id);
	}

	this.getStudents = function(facId) {
		return $http.get(url + "/" + facId + "/students");
	}
});
