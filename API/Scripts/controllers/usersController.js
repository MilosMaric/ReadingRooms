app.controller('usersController', ['$scope', '$window', 'userService', 'universityService', 'facultyService',
                                  function($scope, $window, userService, universityService, facultyService){

    function init() {
      $scope.$parent.checkSession($scope.setHeading);
      $scope.$parent.closeMsnger();
      $scope.users = [];
      $scope.mode = "VIEW";
    }

    $scope.getRole = function() {
      if($scope.$parent.user) {
        var loggedRole = $scope.$parent.user.Role;
      }

      if(loggedRole === "Administrator") {
        $scope.deleteMsg = "U slučaju da obrišete nalog menadžera, obrisaće se i fakultet zajedno sa svim rezervacijama," +
         "čitaonicama i studentima koji mu pripadaju. Da li ste sigurni da želite da obrišete nalog menadžera?"
        return "Manager";
      } else if(loggedRole === "Manager") {
        $scope.deleteMsg = "Da li ste sigurni da želite da obrišete nalog studenta?"
        return "Student";
      } else {
        $scope.deleteMsg = undefined;
        return undefined;
      }
    }

    $scope.setHeading = function() {
      if($scope.$parent.user) {
        var loggedRole = $scope.$parent.user.Role;
      }

      if(loggedRole === "Administrator") {
        $scope.heading = "Menadžeri"
      } else if(loggedRole === "Manager") {
        $scope.heading = "Studenti";
      }

      $scope.refresh();
    }

    $scope.filterByRole = function(allUsers) {
      var role = $scope.getRole();
      var filtered = allUsers.filter(function(item) { return item.Role === role});
      return filtered;
    }

    $scope.refresh = function() {
      var role = $scope.getRole();

      if(role === "Manager") {
        userService.getAll().then(
          function(response) {
            if(response.data.length) {
              $scope.users = $scope.filterByRole(response.data);
            }
          }
        );
      } else if(role === "Student") {
        userService.getLoggedUser().then(
          function(response) {
            if(response.data) {
              facultyService.getStudents(response.data.FacultyId).then(
                function(response1) {
                  if(response1.data.length > 1) {
                    $scope.users = $scope.filterByRole(response1.data);
                  } else {
                    $scope.$parent.showMsg("ERROR", "Nema registrovanih studenata sa fakulteta pod nazivom " + response.data.FacultyName);
                    $window.location.href = "#/profile";
                  }
                }
              );
            }
          }
        );
      }
    }


    init();

    $scope.passMode = function(user) {
      $scope.passUser = angular.copy(user);
      $scope.random = {newPassword : ""};
      $scope.mode = "PASS";
    }

    $scope.viewMode = function() {
      $scope.passUser = undefined;
      $scope.mode = "VIEW";
    }

    $scope.addMode = function() {
      $scope.newFac = {};
      $scope.newManager = {};

      universityService.getAll().then(
        function(response){
          if(response.data.length) {
            $scope.unis = response.data;
            $scope.mode = "ADD";
          } else {
            $scope.$parent.showMsg("ERROR", "Morate prvo uneti univerzitet!");
          }
        }, function() {
          $scope.$parent.showMsg("ERROR", "Došlo je do greške prilikom dobavljanja univerziteta!");
        }
      );

    }

    $scope.changePassword = function() {
      if($scope.random.newPassword.trim()) {
        $scope.passUser.Password = $scope.random.newPassword;
        userService.update($scope.passUser.Id, $scope.passUser).then(
          function() {
            $scope.$parent.showMsg("SUCCESS", "Lozinka je uspešno promenjena.");
            $scope.viewMode();
          }, function() {
            $scope.$parent.showMsg("ERROR", "Lozinka nije uspešno promenjena.");
          }
        );
      } else {
        $scope.$parent.showMsg("ERROR", "Morate uneti novu lozinku.");
      }
    }

    $scope.delete = function(facId, userId,  idx) {
      if($scope.deleteMsg && confirm($scope.deleteMsg)) {
        if($scope.$parent.user.Role === "Administrator") {
          facultyService.delete(facId).then(
            function() {
              $scope.users.splice(idx, 1);
              $scope.$parent.showMsg("SUCCESS", "Uspešno obrisan korisnik.");
            }
          );
        } else if($scope.$parent.user.Role === "Manager"){
          userService.delete(userId).then(
            function() {
              $scope.users.splice(idx, 1);
              $scope.$parent.showMsg("SUCCESS", "Uspešno obrisan korisnik.");
            }
          );
        }
      }
    }

    $scope.addManager = function() {
      $scope.newManager.Role = "Manager";
      facultyService.add($scope.newFac).then(
        function (response) {
          if(response.data) {
            $scope.newManager.FacultyId = response.data.Id;
            userService.add($scope.newManager).then(
              function (response1) {
                if(response1.data) {
                  $scope.users.push(response1.data);
                  $scope.viewMode();
                } else {
                  $scope.$parent.showMsg("ERROR", "Neuspešno dodavanje menadžera.");
                }
              }
            );
          }
        }
      );
    }

}]);
