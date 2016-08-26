app.controller('profileController', ['$scope', '$window', 'userService', 'facultyService',
                                      function($scope, $window, userService, facultyService){

    function init() {
      $scope.$parent.checkSession($scope.getLoggedUser);
      $scope.mode = "VIEW";
    }

    $scope.getLoggedUser = function() {
      userService.getLoggedUser().then(
        function(response) {
          if(response.data) {
            if(response.data) {
              $scope.loggedUser = response.data;
              if($scope.loggedUser.Role === "Student") {
                facultyService.getAll().then(
                  function(response) {
                    if(response.data) {
                      $scope.faculties = response.data;
                    }
                  }
                );
              }
            }
          }
        }
      );
    }

    init();

    $scope.viewMode = function() {
      $scope.mode = "VIEW";
      $scope.editedUser = {};
    }

    $scope.editMode = function() {
      $scope.mode = "EDIT";
      $scope.confirm = {};
      $scope.editedUser = angular.copy($scope.loggedUser);
    }

    $scope.editProfile = function() {
      if($scope.isFormValid()) {
        userService.update($scope.editedUser.Id, $scope.editedUser).then(
          function() {
            $scope.loggedUser = angular.copy($scope.editedUser);
            $scope.viewMode();
            alert("Uspešno ste ažurirali nalog.");
          }
        );
      }
    }

    $scope.deleteUser = function () {
        /*if(confirm("Da li ste sigurni da želite da obrišete nalog na koji ste trenutno prijavljeni?")) {
          userService.delete($scope.loggedUser.Id).then(
            function () {
              $scope.$parent.logout();
              alert("Uspešno ste obrisali nalog.");
            }, function () {
              alert("Došlo je do problema prilikom brisanja naloga.");
            }
          );
        }*/
    }

    $scope.isFormValid = function() {
      if(!$scope.confirm.oldPassword) {
        alert("Morate uneti staru lozinku!");
        return false;
      } else if($scope.confirm.oldPassword.trim() !== $scope.loggedUser.Password) {
        alert("Niste uneli ispravnu staru lozinku");
        return false;
      } else if($scope.editedUser.Role === "Student" && (!$scope.editedUser.Index || $scope.editedUser.Index.trim().length === 0)) {
        alert("Morate uneti oznaku indeksa.");
        return false;
      } else {
        if($scope.confirm.newPassword || $scope.confirm.confirmedNewPassword) {
          if($scope.confirm.newPassword !== $scope.confirm.confirmedNewPassword) {
            alert("Nova lozinka mora dva puta biti unešena");
            return false;
          } else {
            $scope.editedUser.Password = $scope.confirm.newPassword;
          }
        }
      }
      return true;
    }
}]);
