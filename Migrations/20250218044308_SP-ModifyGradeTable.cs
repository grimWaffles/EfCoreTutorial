using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreTutorial.Migrations
{
    /// <inheritdoc />
    public partial class SPModifyGradeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_UpdateGradesInBulk =
            @"
                create procedure dbo.UpdateGradesInBulk
	                @DateTime date
                as
                begin
	                Delete from Grades where GradeName in ('F','G','H')

	                Insert into Grades(GradeName)
	                values('F')
	                Insert into Grades(GradeName)
	                values('G')
	                Insert into Grades(GradeName)
	                values('H')

                    Update Students set DateOfBirth = @DateTime
                end
            ";

            migrationBuilder.Sql(sp_UpdateGradesInBulk);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var delete_sp_UpdateGradesInBulk = @"drop procedure [UpdateGradesInBulk]";

            migrationBuilder.Sql(delete_sp_UpdateGradesInBulk);
        }
    }
}
