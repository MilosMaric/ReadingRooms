app.service('facultyService', function($http){
	var url = '/api/faculty';

	this.getAll = function(){
		return $http.get(url);
	}

	this.getById = function(id){
		return $http.get(url + "/" + id);
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

	this.getReadingRoom = function(facId) {
		return $http.get(url + "/" + facId + "/rrooms");
	}
});
