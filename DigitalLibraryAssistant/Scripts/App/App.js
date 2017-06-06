var app = angular.module("app", ["ngRoute"]);
app.config(function ($routeProvider, $locationProvider) {
	var templatesDir = "/Templates/";
	$routeProvider
    .when("/", {
    	templateUrl: templatesDir + "Index.html",
    	controller: "homeController"
    })
    .when("/test", {
        templateUrl: templatesDir + "Test.html",
        controller: "testController"
        })
    .when("/dashboard", {
        templateUrl: templatesDir + "Dashboard.html",
        controller: "dashboardController"
        })
    .when("/search", {
        templateUrl: templatesDir + "Search.html",
        controller: "searchController"
    })
    .when("/addItem", {
        templateUrl: templatesDir + "AddItem.html",
        controller: "itemController"
    }) 
    .otherwise({
    	redirectTo: "/"
    });
});



app.controller('homeController', function ($scope, apiService, $location, $http) {
 
    $scope.message = "This message is from the Home controller";
    initialize();
    
    $scope.login = function () {

        $scope.Authenticate();          

    };

    //Authenticate based on username and password
    $scope.Authenticate = function () {
        $http.post('/api/Solr', '"' + 'UserName:' + $scope.formData.username + ' AND Password:' + $scope.formData.password + '"').
            then(function (response) {
                console.log(response.data);
                if (response.data.length == 1) {
                    $scope.isAuthenticated = true;
                    $location.path('/dashboard');
                    $scope.username = response.data[0].UserName;
                    if (response.data[0].RoleTypeDesc == 'Admin'){
                        $scope.isAdmin = true;                        
                    }
                    $scope.firstname = response.data[0].FirstName;
                    $scope.lastname = response.data[0].LastName
                    $scope.GetItems();                                        
                }
            },
            function (response) {
                alert("Issue with POST to SOLR")
            });
    }


    //GetItems for Dashboard
    $scope.GetItems = function () {
        var queryString;
        if ($scope.isAdmin == true) {
            queryString = '"' + 'DataTableName:Items AND Loaned:Yes' + '"'
        }
        else {
            queryString = '"' + 'DataTableName:Items AND UserName:'+$scope.username + '"'
        }

        $http.post('/api/Solr', queryString).
            then(function (response) {                
                console.log(response.data);
                $scope.items = response.data;
                var notifications = [] ;
                var enddate;
                var todayDate = new Date().toISOString();        
                //handle notifications logic
                for (i = 0; i < response.data.length; i++) {
                    date = response.data[i].LoanEndDate
                    if (todayDate > date) {
                        console.log("Greater date");
                        notifications.push({ "Message" : "The Date has passed, please return the book."})
                    }
                }
                $scope.notif = notifications;
                $scope.notifLength = notifications.length;

            },
            function (response) {                
                alert("Issue with POST to SOLR")
            });
    }

    function initialize () {
        $scope.formData = {};
        $scope.q = {};
        $scope.isAdmin = false;
        $scope.isAuthenticated = false;

    }


    $scope.Search = function () {      
        $location.path('/search');

        var queryString;

        if ($scope.q.queryString != undefined && $scope.q.queryString != ''){
            queryString = '"' + $scope.q.queryString + ' AND DataTableName:Items' + '"';
        }
        else {
            queryString = '"' + 'DataTableName: Items' + '"';
        }
        $http.post('/api/Solr', queryString).
            then(function (response) {
                console.log(response.data);
                $scope.SearchResults = response.data;
            },
            function (response) {
                alert("Issue with POST to SOLR")
            });
    }

    $scope.routeToDashboard = function () {
        $location.path('/dashboard');
    }
});

app.controller('testController', function ($scope) {
	$scope.message = "This message is from the Test controller";
});

app.factory("apiService", function ($http) {
	var url = "/api/";

	return {
		GetRequest: function (path, parameters) {
			$http({
				method: 'GET',
				url: url + path + "/" + parameters
			})
            .then(function successCallback(response) {
                alert("Success");
            },
            function errorCallback(response) {
                alert("Failure");
            });
        }
    }
});

app.controller('dashboardController', function ($scope) {

});

app.controller('searchController', function ($scope) {
 
});

function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

app.controller("itemController", function ($scope, $http, $location) {
    $scope.Item = {};

    $scope.addItem = function () {

        var item = {
            "BookId": 0,
            "Title": $scope.Item.Title,
            "Author": $scope.Item.Author,
            "ISBN": $scope.Item.ISBN,
            "ImageUrl": $scope.Item.ImageUrl,
            "id": guid(),
            "IsNew": "1",
            "DataTableName": "Items",
            "Loaned": "no",
            "CreateDate": new Date()
        }

        $http.post('/api/AddItem', JSON.stringify(item)).
            then(function (response) {
                $location.path('/dashboard');
            },
            function (response) {
                alert("Issue with POST to SOLR")
            });
    };


});
