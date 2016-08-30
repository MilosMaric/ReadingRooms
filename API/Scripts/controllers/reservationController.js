app.controller('reservationController', ['$scope', '$window', 'userService', 'reservationService', 'facultyService', 'rroomsService',
                function($scope, $window, userService, reservationService, facultyService, rroomsService){

    function init() {
      $scope.$parent.checkSession($scope.getReservations);
      $scope.$parent.closeMsnger();
      $scope.dpOptions = {
        minDate : $scope.addDays(0),
        maxDate : $scope.addDays(8),
        maxMode : "day"
      }
      $scope.freeSeats = -1;
      $scope.mode = "VIEW";
      $scope.popup = {isOpened : false};
      $scope.reservations = [];
    }


    $scope.getReservations = function() {
      userService.getLoggedUser().then(
        function(response) {
          if(response.data) {
            if(response.data.Role !== "Student") {
              $window.location.href = "#/profile";
            } else {
              $scope.user = response.data;
              $scope.getMyRroom();
              userService.getStudentReservations(response.data.Id).then(
                function(response1) {
                  $scope.viewMode();
                  if(response1.data && response1.data.length > 0) {
                    $scope.reservations = response1.data;
                  }
                }
              );
            }
          }
        }
      );
    }


    $scope.getMyRroom = function() {
      if(!$scope.rroom) {
        facultyService.getReadingRoom($scope.user.FacultyId).then(
          function(response1) {
            if(response1.data && response1.data.length > 0) {
              $scope.rroom = response1.data[0];
            }
          }
        );
      }
    }

    $scope.addDays = function(days) {
      var day = new Date();
      return day.setDate(day.getDate() + days);
    }

    init();

    $scope.viewMode = function() {
      $scope.mode = "VIEW";
      $scope.newRes = {};
      $scope.freeSeats = -1;
    }

    $scope.addMode = function() {
      $scope.mode = "ADD";
      $scope.rroom.WorkingTimeFrom = new Date(Date.parse(angular.copy($scope.rroom.WorkingTimeFrom)));
      $scope.rroom.WorkingTimeTo = new Date(Date.parse(angular.copy($scope.rroom.WorkingTimeTo)));
      $scope.newRes = {
        ETA: angular.copy($scope.rroom.WorkingTimeFrom),
        ETD: angular.copy($scope.rroom.WorkingTimeTo)
      };
    }

    $scope.formatTime = function(date) {
      var d = new Date(Date.parse(date));
      return d.toTimeString().substr(0, 5);
    }

    $scope.formatDate = function(date) {
      var d = new Date(Date.parse(date));
      return d.toLocaleString("sr-rs").split(' ')[0];
    }

    $scope.checkSeats = function() {
      $scope.setET();
      rroomsService.checkSeats($scope.rroom.Id, $scope.newRes.ETA, $scope.newRes.ETD).then(
        function(response) {
          $scope.freeSeats = response.data;
        }
      );
    }

    $scope.delete = function(id, idx) {
      $scope.deleteInfo = { id : id, idx : idx};
      $scope.$parent.openModal(
        "Da li ste sigurni da želite da otkažete rezervaciju?",
        $scope.deleteCallback
      );
    }

    $scope.deleteCallback = function() {
      reservationService.delete($scope.deleteInfo.id).then(
        function() {
          $scope.reservations.splice($scope.deleteInfo.idx, 1);
        }
      );
    }

    $scope.refresh = function() {
      userService.getStudentReservations($scope.user.Id).then(
        function(response1) {
          $scope.viewMode();
          if(response1.data && response1.data.length > 0) {
            $scope.reservations = response1.data;
          }
        }
      );
    }

    $scope.reserve = function() {
      $scope.newRes.UserId = $scope.user.Id;
      $scope.newRes.ReadingRoom = { Id : $scope.rroom.Id };
      $scope.newRes.Day = undefined;
      reservationService.add($scope.newRes).then(
        function(response) {
          if(response.data) {
            $scope.reservations.push(response.data);
          } else {
            $scope.$parent.showMsg("ERROR", "Došlo je do greške prilikom rezervisanja.");
          }
          $scope.viewMode();
        }
      );
    }

    $scope.setET = function() {
      var newETA = angular.copy($scope.newRes.Day);
      var newETD = angular.copy($scope.newRes.Day);

      newETA.setHours($scope.newRes.ETA.getHours());
      newETA.setMinutes($scope.newRes.ETA.getMinutes());

      newETD.setHours($scope.newRes.ETD.getHours());
      newETD.setMinutes($scope.newRes.ETD.getMinutes());

      $scope.newRes.ETA = newETA;
      $scope.newRes.ETD = newETD;
    }

    $scope.timeChanged = function () {
        if($scope.freeSeats != -1) {
          $scope.freeSeats = -1;
        }
    }

    $scope.readyToCheck = function() {
      return $scope.newRes.ETA && $scope.newRes.ETD && $scope.newRes.Day && !$scope.checkETA() && !$scope.checkETD();
    }

    $scope.checkETA = function() {
      return $scope.newRes.ETA >= $scope.newRes.ETD || $scope.later($scope.newRes.ETA, $scope.rroom.WorkingTimeFrom) === 2;
    }

    $scope.checkETD = function() {
      return $scope.newRes.ETA >= $scope.newRes.ETD || $scope.later($scope.newRes.ETD, $scope.rroom.WorkingTimeTo) === 1;
    }

    $scope.open = function() {
      $scope.popup.isOpened = true;
    }

    $scope.later = function (f, s) {
      var n = 0;
      var fh = f.getHours(), fm = f.getMinutes(), sh = s.getHours(), sm = s.getMinutes();

      if(sh > fh) {
        n = 2;
      } else if(fh > sh) {
        n = 1;
      } else {
        if(fm > sm) {
          n = 1;
        } else if(sm > fm) {
          n = 2;
        }
      }

      return n;
    }
}]);
