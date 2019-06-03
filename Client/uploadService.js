var uploadService = function ($http, $q) {
    return {
        uploadFiles: function ($scope) {

            var request = {
                method: 'POST',
                url: '/api/upload/',
                data: $scope.formdata,
                headers: {
                    'Content-Type': undefined
                }
            };

            // SEND THE FILES.
            return $http(request)
                .then(
                function (response) {
                    if (typeof response.data === 'string') {
                        return response.data;
                    } else {
                        return $q.reject(response.data);
                    }
                },
                function (response) {
                    return $q.reject(response.data);
                }
                );
        }

    };

}
