// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function isNewTab() {
    const SINGLE_PAGE_HISTORY_LENGTH = 1;
    return window.history.length <= SINGLE_PAGE_HISTORY_LENGTH;
}