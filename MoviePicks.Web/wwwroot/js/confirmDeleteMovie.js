function confirmDeleteMovie(e, deleteMovieUrl, movieName, movieThumbnailUrl) {
    e.preventDefault();

    Swal.fire({
        title: 'Delete this from your collection?',
        html: `
            <h4>${movieName}</h4>
            <img src="${movieThumbnailUrl}" alt="Movie Thumbnail" class="movie-thumbnail"/>
            <p class="warning-message-margin-top">Your review will be deleted too.</p>
        `,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',     // red confirm button
        cancelButtonColor: '#3085d6',   // blue cancel button
        confirmButtonText: 'Delete',
        focusCancel: true
    }).then((alertResult) => {
        if (alertResult.isConfirmed) {
            $.ajax({
                url: deleteMovieUrl,
                type: 'DELETE',
                success: function (response) {

                },
                error: function (request, msg, error) {

                }
            }).always(function (response) {
                window.location.href = response.redirectUrl;
            });
        }
    })
}
