app.service('rroomsService', function($http){
	var url = '/api/rroom';

	this.add = function(rroom){
		return $http.post(url, rroom);
	}
});
