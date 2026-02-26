// TODO: For best security, restrict CORS access to just the domain we need. Currently, "*" allows access to all external domains.
const header = new Headers({ "Access-Control-Allow-Origin": "*" });
// TODO: Try using this to limit CORS access to only the single external domain needed.
//const header = new Headers({ "Access-Control-Allow-Origin": "http://www.omdbapi.com" });

const movieSearchBox = document.getElementById("search-text");
const movieCardsContainer = document.getElementById("movie-cards-container");
const movieDetailsContainer = document.getElementById("movie-details-container");
const errorDisplay = document.getElementById("error-loading-movies-by-title");

// Form user input elements

const imdbIdInput = document.getElementById("imdbId-input");
const genreInputField = document.getElementById("genre-input");
const viewerRatingInput = document.getElementById("viewer-rating");
const reviewHeadingInput = document.getElementById("review-heading-input");
const reviewCommentsInput = document.getElementById("review-comments-input");

const backendServiceBaseUrl = `http://localhost:5053/api/Omdb/`;

const showMovieDetailsContainer = () => {
    movieDetailsContainer.classList.remove("hide-content");
}

const hideMovieDetailsContainer = () => {
    movieDetailsContainer.classList.add("hide-content");
}

async function fetchMoviesArray(titlePatternToSearch) {
    try {
        const relativeUrl = `movies/${titlePatternToSearch}`;
        const url = `${backendServiceBaseUrl}${relativeUrl}`;

        // Header provided to enable CORS. This allows fetching movie data on the external omdbapi.com domain.
        // Otherwise a CORS error would occur.
        // Cross-origin resource sharing (CORS) is an HTTP feature that enables a web application running under
        // one domain to access resources in another domain.
        const response = await fetch(url, { header: header });

        if (!response.ok) {
            console.log(`OMDB movie fetch response is false for URL ${url}`);
            const message = `An error has occured: ${response.status}`;
            throw new Error(message);
        }

        const searchResultMoviesArray = await response.json();
        return searchResultMoviesArray
    }
    catch (err) {
        errorDisplay.innerHTML = err.message;
        console.log(`OMDB movie fetch failed. URL ${url}`);
    }

    return [];
}

// Returns movie details for the specified IMDB movie id. Return null if unable to retrieve the movie details.
// Writes an error message if an HTTP error occurs.
// Write an informational message if unable to retrieve the movie details when the HTTP request is successful.
async function fetchMovieDetails(imdbId) {
    try {
        const relativeUrl = `movie?imdbId=${imdbId}`;
        const url = `${backendServiceBaseUrl}${relativeUrl}`;

        const response = await fetch(url, { header: header });

        if (!response.ok) {
            console.log(`HTTP response is not OK when fetching movie details. HTTP Response Status: ${response.status}, URL ${url}`);
            const message = `A connection error has occured fetching details of a movie.`;
            throw new Error(message);
        }

        const searchResult = await response.json();

        if (searchResult.Response == "True") {
            const movieDetails = searchResult;
            return movieDetails;
        }
        else {
            console.log(`OMDB movie response is false for fetching movie details. URL ${url}`);
            const message = `Unable to fetch details of a movie.`;
            throw new Error(message);
        }
    }
    catch (err) {
        errorDisplay.innerHTML = err.message;
        console.log(`OMDB movie fetch failed. URL ${url}`);
    }

    return null;
}

// Return the absolute URL to the NO IMAGE file to display when a movie does not have a poster
const getMoviePosterNotFoundImage = () => {

    // This is a static file provided at wwwroot location "~/images/No_Image.jpg"
    const noimagefile = "/images/No_Image.jpg";

    const noImageUrl = new URL(noimagefile, document.location);
    return noImageUrl;
}

const getNormalizedMoviePosterUrl = (moviePoster) => {
    let moviePosterUri = getMoviePosterNotFoundImage();

    if (moviePoster != "N/A") {
        moviePosterUri = moviePoster;
    }

    return moviePosterUri;
}

function displayMovies(moviesArray) {
    let movieCardList = "";
    moviesArray.forEach(movie => {
        const moviePosterUri = getNormalizedMoviePosterUrl(movie.Poster);

        movieCardList += `
                        <div class="mx-0 mb-1 col-sm-12 col-md-8 col-lg-4">
                            <div class="movie-item" data-imdbID="${movie.imdbID}" onclick="movieOnClickHandler(this)">
                                <div class="movie-thumbnail-poster-image growth-container">
                                    <img class="grow" src="${moviePosterUri}" alt="Thumbnail poster of movie ${movie.Title}, ${movie.Year}"/>
                                </div>
                                <div class="movie-item-details">
                                    <h6 class="movie-item-title">${movie.Title}</h6>
                                    <p  class="movie-item-title">${movie.Type}</p>
                                    <p  class="movie-item-title">${movie.Year}</p>
                                </div>
                            </div>
                        </div>
                        `
    });

    movieCardsContainer.innerHTML = movieCardList;
    movieCardsContainer.classList.remove("hide-content");
}

class MovieDetailsRenderer {
    constructor() {
        // Form input elements
        this.imdbIdInput = document.getElementById("imdbId-input");
        this.genreInputField = document.getElementById("genre-input");
        this.viewerRatingInput = document.getElementById("viewer-rating");
        this.reviewHeadingInput = document.getElementById("review-heading-input");
        this.reviewCommentsInput = document.getElementById("review-comments-input");
        this.formInputFieldsArray = [this.imdbIdInput,
                                     this.genreInputField,
                                     this.viewerRatingInput,
                                     this.reviewHeadingInput,
                                     this.reviewCommentsInput]

        // Movie details html elements
        this.moviePosterImg = document.getElementById("movie-details-poster-image");
        this.title = document.getElementById("movie-title");
        this.productYear = document.getElementById("product-year");
        this.runtime = document.getElementById("runtime");
        this.rated = document.getElementById("rated");
        this.genre = document.getElementById("genre");
        this.director = document.getElementById("director");
        this.actors = document.getElementById("actors");
        this.plot = document.getElementById("plot");
        this.language = document.getElementById("language");
        this.productType = document.getElementById("product-type");
    }

    enableFormInputFields() {
        this.formInputFieldsArray.forEach(inputField => inputField.removeAttribute("disabled"));
    }

    disableFormInputFields() {
        this.formInputFieldsArray.forEach(inputField => inputField.setAttribute("disabled", ""));
    }

    // Set form input for newly selected movie.
    setFormInput(movieDetails) {
        this.clearFormInput();
        this.imdbIdInput.value = movieDetails.imdbID;
        this.genreInputField.value = movieDetails.Genre;
    }

    clearFormInput() {
        this.imdbIdInput.value = "";
        this.genreInputField.value = "";
        this.viewerRatingInput.value = "";
        this.reviewHeadingInput.value = ""
        this.reviewCommentsInput.value = "";
    }

    switchToFormEditingMode(movieDetails) {
        this.enableFormInputFields();
        this.setFormInput(movieDetails)
    }

    switchToSearchMode() {
        this.clearFormInput();
        this.disableFormInputFields();
    }

    clearMovieDetails() {
        this.moviePosterImg.setAttribute("src", "");
        this.title.value = "";
        this.productYear.value = "";
        this.runtime.value = "";
        this.rated.value = "";
        this.genre.value = "";
        this.director.value = "";
        this.actors.value = "";
        this.plot.value = "";
        this.language.value = "";
        this.productType.value = "";
    }

    displaySelectedMovie(movieDetails) {
        // Display movie details on content area

        const moviePosterUrl = getNormalizedMoviePosterUrl(movieDetails.Poster);
        this.moviePosterImg.setAttribute("src", moviePosterUrl);
        this.title.innerHTML = movieDetails.Title;

        this.productYear.innerHTML = movieDetails.Year;
        this.runtime.innerHTML = movieDetails.Runtime;
        this.rated.innerHTML = movieDetails.Rated;

        if (this.genre.value != "N/A") {
            this.genre.innerHTML = movieDetails.Genre;
        }

        this.director.innerHTML = movieDetails.Director;
        this.actors.innerHTML = movieDetails.Actors;
        this.plot.innerHTML = movieDetails.Plot;
        this.language.innerHTML = movieDetails.Language;
        this.productType.innerHTML = movieDetails.Type;

        showMovieDetailsContainer();
    }
}

const selectedMovieRenderer = new MovieDetailsRenderer();

// Get movie details handler
const movieOnClickHandler = async (movieItemElement) => {
    movieCardsContainer.innerHTML = ""; // Remove searched movies 
    movieCardsContainer.classList.add("hide-content"); // Hide searched movies by hiding their container
    movieSearchBox.value = "";
    const imdbId = movieItemElement.getAttribute("data-imdbID");
    const movieDetails = await fetchMovieDetails(imdbId)

    selectedMovieRenderer.displaySelectedMovie(movieDetails);
    selectedMovieRenderer.switchToFormEditingMode(movieDetails);
}

async function getMovieDetails() {
    const movieItemsArray = movieCardsContainer.querySelectorAll(".movie-item");
    movieItemsArray.forEach(movie => {
    });
}

async function searchMovies() {
    hideMovieDetailsContainer();
    const titleToSearch = movieSearchBox.value.trim();

    if (titleToSearch.length > 0) {
        selectedMovieRenderer.clearMovieDetails();
        selectedMovieRenderer.switchToSearchMode();

        const moviesArray = await fetchMoviesArray(titleToSearch);
        displayMovies(moviesArray);
    }
    else {
        movieCardsContainer.innerHTML = "";
        selectedMovieRenderer.switchToSearchMode();
    }
}