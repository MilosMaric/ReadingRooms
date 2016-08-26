app.controller('facultyController', ['$scope', '$window', 'universityService', 'userService', 'facultyService',
                function($scope, $window, universityService, userService, facultyService){

  function init() {
    $scope.$parent.checkSession($scope.getLoggedUser);
    $scope.$parent.closeMsnger();
    $scope.mode = "VIEW";
  }

  $scope.getLoggedUser = function() {
    userService.getLoggedUser().then(
      function(response) {
        if(response.data) {
          if(response.data.Role !== "Manager") {
            $window.location.href = "#/profile";
          } else {
            facultyService.getById(response.data.FacultyId).then(
              function(response1) {
                if(response1.data) {
                  $scope.faculty = response1.data;
                }
              }
            );
          }
        }
      }
    );
  }

  init();

  $scope.editMode = function() {
    $scope.mode = "EDIT";
    $scope.newFac = angular.copy($scope.faculty);
    $scope.getAllUnis();
  }

  $scope.viewMode = function() {
    $scope.mode = "VIEW";
    $scope.newFac = {};
  }

  $scope.getAllUnis = function() {
    universityService.getAll().then(
      function (response) {
        if(response.data) {
          $scope.unis = response.data;
        }
      }
    );
  }

  $scope.editFaculty = function() {
    if($scope.formIsValid()) {
      facultyService.update($scope.newFac.Id, $scope.newFac).then(
        function() {
            $scope.changeUniName();
            $scope.viewMode();
            $scope.$parent.showMsg("SUCCESS", "Uspešno ste ažurirali informacije o fakultetu.");
        }
      );
    }
  }

  $scope.changeUniName = function() {
    if($scope.newFac.UniversityId !== $scope.faculty.UniversityId) {
      for(var i = 0; i < $scope.unis.length; i++) {
        if($scope.unis[i].Id+"" === $scope.newFac.UniversityId) {
          $scope.newFac.UniversityName = $scope.unis[i].Name;
          $scope.faculty = angular.copy($scope.newFac);
          break;
        }
      }
    }
  }

  $scope.formIsValid = function() {
    if($scope.newFac.Name && $scope.newFac.Name.trim().length > 0 &&
      $scope.newFac.Address && $scope.newFac.Address.trim().length > 0) {
        return true;
    }
    $scope.$parent.showMsg("ERROR", "Moraju i naziv i adresa fakulteta biti popunjeni!");

    return false;
  }

}]);
