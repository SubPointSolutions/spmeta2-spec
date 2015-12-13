
$(document).ready(function () {

    // init tree for all tree-grid tables
    // http://maxazan.github.io/jquery-treegrid/
    $('.tree').treegrid();

    $(function () {
        $('[data-toggle="popover"]').popover({ trigger: "hover" });
    });

    //$('body').scrollspy({ target: "#myScrollspy", offset: 50 });
});