app.service('universityService', function($http){
	var url = '/api/uni';

	this.getAll = function(user){
		return $http.get(url);
	}

	this.add = function(uni){
		return $http.post(url, uni);
	}

  this.update = function(id, uni){
		return $http.put(url + "/" + id, uni);
	}

  this.delete = function(id){
		return $http.delete(url + "/" + id);
	}
});
