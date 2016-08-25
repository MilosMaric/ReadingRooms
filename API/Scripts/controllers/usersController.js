app.controller('usersController', ['$scope', '$window', 'userService', 'universityService', 'facultyService',
                                  function($scope, $window, userService, universityService, facultyService){

    function init() {
      $scope.$parent.checkSession($scope.setHeading);
      $scope.users = [];
      $scope.mode = "VIEW";
      userService.getAll().then(
        function(response) {
          if(response.data.length) {
            $scope.users = $scope.filterByRole(response.data);
          }
        }
      );
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
    }

    $scope.filterByRole = function(allUsers) {
      var role = $scope.getRole();
      var filtered = allUsers.filter(function(item) { return item.Role === role});
      return filtered;
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
            alert("Morate prvo uneti univerzitet!");
          }
        }, function() {
          alert("Došlo je do greške prilikom dobavljanja univerziteta!");
        }
      );

    }

    $scope.changePassword = function() {
      if($scope.random.newPassword.trim()) {
        $scope.passUser.Password = $scope.random.newPassword;
        userService.update($scope.passUser.Id, $scope.passUser).then(
          function() {
            alert("Lozinka je uspešno promenjena.");
            $scope.viewMode();
          }, function() {
            alert("Lozinka nije uspešno promenjena.");
          }
        );
      } else {
        alert("Morate uneti novu lozinku.");
      }
    }

    $scope.delete = function(facId, userId,  idx) {
      if($scope.deleteMsg && confirm($scope.deleteMsg)) {
        if($scope.$parent.user.Role === "Administrator") {
          facultyService.delete(facId).then(
            function() {
              $scope.users.splice(idx, 1);
              alert("Uspešno obrisan korisnik.");
            }
          );
        } else if($scope.$parent.user.Role === "Manager"){
          userService.delete(userId).then(
            function() {
              $scope.users.splice(idx, 1);
              alert("Uspešno obrisan korisnik.");
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
                  alert("Neuspešno dodavanje menadžera.");
                }
              }
            );
          }
        }
      );
    }

    $scope.refresh = function() {
      userService.getAll().then(
        function(response) {
          if(response.data.length) {
            $scope.users = $scope.filterByRole(response.data);
          }
        }
      );
    }
}]);
