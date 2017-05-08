(function ($) {

    $(document).ready(function () {
        Courses = Backbone.Collection.extend({
            url: '/api/catalog'
        });

        var courses = new Courses();
        courses.fetch({
            success: function (data_array) {
                _.each(data_array.models, function (data) {
                    console.log(data.attributes);
                    var catalogTitle = "<p>" + data.attributes.Title + "</p>";
                    $('#catalog').append(catalogTitle);
                });
            }
        });

        function post_data_to_the_server() {
            var courses = new Courses();
            courses.fetch({ data: { course_name: $('#course_type').val() }, type: 'POST' });
            $('#course_type').val('');
        }
    });

})(jQuery);