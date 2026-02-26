using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieRatingsBackendWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImdbId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewHeading = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ReviewComments = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenres",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MovieGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Comedy" },
                    { 3, "Drama" },
                    { 4, "Thriller" },
                    { 5, "Sci-Fi" },
                    { 6, "Romance" },
                    { 7, "Horror" },
                    { 8, "Adventure" },
                    { 9, "Mystery" },
                    { 10, "Fantasy" },
                    { 11, "Crime" },
                    { 12, "Short" },
                    { 13, "Biography" },
                    { 14, "War" },
                    { 15, "Music" },
                    { 16, "Animation" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "ImdbId", "Rating", "ReviewComments", "ReviewHeading" },
                values: new object[,]
                {
                    { 1, "tt3581920", 5, "Movie1 review", "Heading: The Last of Us2" },
                    { 2, "tt1188729", 10, "Movie2 review", "Heading: Pandorum" },
                    { 3, "tt0348150", 1, "Movie3 review", "Heading: Superman Returns" },
                    { 4, "tt6565702", 8, "Movie4 review", "Heading: Dark Phoenix" },
                    { 5, "tt0100813", 3, "Movie5 review", "Heading: Treasure Island" }
                });

            migrationBuilder.InsertData(
                table: "MovieGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 6, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 7, 2 },
                    { 5, 3 },
                    { 8, 3 },
                    { 9, 4 },
                    { 1, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieGenres_GenreId",
                table: "MovieGenres",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieGenres");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
