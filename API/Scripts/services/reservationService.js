app.service('reservationService', function($http){
	var url = '/api/reservation';

	this.add = function(reservation) {
		return $http.post(url, reservation);
	}

  this.delete = function(id){
		return $http.delete(url + "/" + id);
	}
});
