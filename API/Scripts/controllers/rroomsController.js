app.controller('rroomsController', ['$scope', '$window', 'userService', 'facultyService', 'rroomsService',
                function($scope, $window, userService, facultyService, rroomsService){

    function init() {
      $scope.$parent.checkSession($scope.getReadingRoom);
      $scope.$parent.closeMsnger();
      $scope.mode = "IDLE";
    }

    $scope.getReadingRoom = function() {
      userService.getLoggedUser().then(
        function(response) {
          if(response.data) {
            if(response.data.Role === "Administrator") {
              $window.location.href = "#/profile";
            } else {
              $scope.user = response.data;
              facultyService.getReadingRoom(response.data.FacultyId).then(
                function(response1) {
                  if(response1.data && response1.data.length > 0) {
                    $scope.rroom = response1.data[0];
                    $scope.viewMode();
                  } else if($scope.user.Role === "Manager"){
                    $scope.initRroom();
                    $scope.editMode();
                  } else if($scope.user.Role === "Student"){
                    alert("Menadžer vašeg fakulteta još nije uneo podatke o čitaonici. Bićete preusmereni na stranicu vašeg profila.");
                    $window.location.href = "#/profile";
                  }
                }
              );
            }
          }
        }
      );
    }

    $scope.initRroom = function() {
      $scope.newRroom = {};
      $scope.newRroom.WorkingTimeFrom = new Date();
      $scope.newRroom.WorkingTimeFrom.setHours(0);
      $scope.newRroom.WorkingTimeFrom.setMinutes(1);

      $scope.newRroom.WorkingTimeTo = new Date();
      $scope.newRroom.WorkingTimeTo.setHours(23);
      $scope.newRroom.WorkingTimeTo.setMinutes(59);
    }

    init();

    $scope.saveRroom = function() {
      $scope.newRroom.FacultyId = $scope.user.FacultyId;
      if($scope.formIsValid()) {
        rroomsService.add($scope.newRroom).then(
          function (response) {
            if(response.data) {
              $scope.rroom = response.data;
              $scope.viewMode();
            }
          }
        );
      }
    }

    $scope.formIsValid = function() {
      if($scope.newRroom.WorkingTimeTo > $scope.newRroom.WorkingTimeFrom && $scope.newRroom.Dimension &&
              (!$scope.newRroom.ChecksIndex ||
                ($scope.newRroom.ChecksIndexFrom < $scope.newRroom.ChecksIndexTo &&
                $scope.newRroom.WorkingTimeFrom <= $scope.newRroom.ChecksIndexFrom &&
                $scope.newRroom.WorkingTimeTo >= $scope.newRroom.ChecksIndexTo)
              )
        ) {
        return true;
      }
      $scope.$parent.showMsg("ERROR", "Morate popuniti dimenziju. Vremena 'od' moraju biti pre vremena 'do'. Opseg provere indeksa mora "+
      "ulaziti u radno vreme. Dimenzija mora biti pozitivan broj.");

      return false;
    }

    $scope.editMode = function() {
      $scope.mode = "EDIT";
      if($scope.rroom) {
        $scope.newRroom = angular.copy($scope.rroom);
      }
    }

    $scope.viewMode = function() {
      $scope.mode = "VIEW";
      $scope.newRroom = {};
    }

    $scope.checksIndexesChange = function() {
      if($scope.newRroom.ChecksIndex) {
        $scope.newRroom.ChecksIndexFrom = angular.copy($scope.newRroom.WorkingTimeFrom);
        $scope.newRroom.ChecksIndexTo = angular.copy($scope.newRroom.WorkingTimeTo);
      }
    }

    $scope.formatTime = function(date) {
      var d = new Date(Date.parse(date));
      return d.toTimeString().substr(0, 5);
    }

    $scope.checkWTFrom = function() {
      return $scope.newRroom.WorkingTimeFrom >=  $scope.newRroom.WorkingTimeTo ||
      ($scope.newRroom.ChecksIndex && $scope.newRroom.WorkingTimeFrom >  $scope.newRroom.ChecksIndexFrom);
    }

    $scope.checkWTTo = function() {
      return $scope.newRroom.WorkingTimeFrom >=  $scope.newRroom.WorkingTimeTo ||
      ($scope.newRroom.ChecksIndex && $scope.newRroom.WorkingTimeTo <  $scope.newRroom.ChecksIndexTo);
    }

    $scope.checkIdxFrom = function() {
      return $scope.newRroom.ChecksIndexFrom >=  $scope.newRroom.ChecksIndexTo ||
      ($scope.newRroom.ChecksIndex && $scope.newRroom.ChecksIndexFrom <  $scope.newRroom.WorkingTimeFrom);
    }

    $scope.checkIdxTo = function() {
      return $scope.newRroom.ChecksIndexFrom >=  $scope.newRroom.ChecksIndexTo ||
      ($scope.newRroom.ChecksIndex && $scope.newRroom.ChecksIndexTo >  $scope.newRroom.WorkingTimeTo);
    }

}]);
