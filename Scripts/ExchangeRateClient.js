var ExchangeRateApp = angular.module('ExchangeRateApp', [])

ExchangeRateApp.controller('ExchangeRateController', function ($scope) {
    getExchangeRates();

    function getExchangeRates() {
        ExchangeRateService.getExchangeRates()
        .success(function (data) {
            $scope.exchangeRates = data;
        })
        .error(function (error) {
            $scope.status = 'Unable to load exchange data: ' + error.message;
        });
    }
});

ExchangeRateApp.factory("ExchangeRateService", ['$http', function ($http) {

    var exchangeRateService = {};

    ExchangeRateService.getExchangeRates = function () {
        return $http.get('/ExchangeRate/GetExchangeRates');
    };
    return ExchangeRateService;

}]);
