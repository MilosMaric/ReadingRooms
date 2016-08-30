app.controller('statusController', ['$scope', '$window', 'rroomsService', function($scope, $window, rroomsService){

    function init() {
      $scope.intervalStep = 1;
      $scope.buttonNames = [
        "Pretraga za narednih sat vremena",
        "Pretraga za naredna 2 sata",
        "Pretraga za naredna 3 sata"
      ]
      $scope.$parent.checkSession($scope.getAllSchemas);
      $scope.$parent.closeMsnger();
      $scope.rrooms = [];
    }

    $scope.getAllSchemas = function() {
      $scope.setTimeIntervalByStep();
      rroomsService.getAllSchemas($scope.ETA, $scope.ETD).then(
        function(response) {
          $scope.rrooms = response.data;
        }, function() {
          $scope.$parent.showMsg('ERROR', "Došlo je do greške prilikom dobavljanja informacija o čitaonicama.")
        }
      );
    }

    $scope.setTimeIntervalByStep = function() {
      var retVal = new Date();
      $scope.ETA = new Date();
      retVal.setHours(retVal.getHours() + $scope.intervalStep);
      $scope.ETD = retVal;
    }

    init();

    $scope.setStep = function(step) {
      $scope.intervalStep = step;
    }

    $scope.formatTime = function(date) {
      var d = new Date(Date.parse(date));
      return d.toTimeString().substr(0, 5);
    }
}]);
