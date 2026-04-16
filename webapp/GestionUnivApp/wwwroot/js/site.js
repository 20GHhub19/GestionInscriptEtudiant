// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('.dropdown-submenu > a.dropdown-toggle').on('click', function (e) {
        e.preventDefault();
        e.stopPropagation();

        var $submenu = $(this).next('.dropdown-menu');

        // Fermer les autres au même niveau
        $(this).parent().siblings('.dropdown-submenu').find('.dropdown-menu.show').removeClass('show');

        // Toggle celui-ci
        $submenu.toggleClass('show');
    });

    // Fermer les sous-menus quand on ferme le dropdown parent
    $('.dropdown').on('hidden.bs.dropdown', function () {
        $(this).find('.dropdown-menu.show').removeClass('show');
    });
});

