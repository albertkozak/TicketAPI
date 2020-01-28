using Microsoft.EntityFrameworkCore.Migrations;

namespace netCoreAPI.Migrations
{
    public partial class intialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketPurchase",
                columns: table => new
                {
                    purchase_id = table.Column<int>(nullable: false),
                    payment_method = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    payment_amount = table.Column<decimal>(type: "money", nullable: false),
                    confirmation_code = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TicketPu__87071CB9297F08D1", x => x.purchase_id);
                });

            migrationBuilder.CreateTable(
                name: "Venue",
                columns: table => new
                {
                    venue_name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    capacity = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venue__3D6847F267E22EBA", x => x.venue_name);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    event_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    event_name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    venue_name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.event_id);
                    table.ForeignKey(
                        name: "FK__Event__venue_nam__4222D4EF",
                        column: x => x.venue_name,
                        principalTable: "Venue",
                        principalColumn: "venue_name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    section_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    section_name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    venue_name = table.Column<string>(unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.section_id);
                    table.ForeignKey(
                        name: "FK__Section__venue_n__398D8EEE",
                        column: x => x.venue_name,
                        principalTable: "Venue",
                        principalColumn: "venue_name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Row",
                columns: table => new
                {
                    row_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    row_name = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    section_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Row", x => x.row_id);
                    table.ForeignKey(
                        name: "FK__Row__section_id__3C69FB99",
                        column: x => x.section_id,
                        principalTable: "Section",
                        principalColumn: "section_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seat",
                columns: table => new
                {
                    seat_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<decimal>(type: "money", nullable: true),
                    row_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seat", x => x.seat_id);
                    table.ForeignKey(
                        name: "FK__Seat__row_id__3F466844",
                        column: x => x.row_id,
                        principalTable: "Row",
                        principalColumn: "row_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventSeat",
                columns: table => new
                {
                    event_seat_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seat_id = table.Column<int>(nullable: false),
                    event_id = table.Column<int>(nullable: false),
                    event_seat_price = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSeat", x => x.event_seat_id);
                    table.ForeignKey(
                        name: "FK__EventSeat__event__45F365D3",
                        column: x => x.event_id,
                        principalTable: "Event",
                        principalColumn: "event_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__EventSeat__seat___44FF419A",
                        column: x => x.seat_id,
                        principalTable: "Seat",
                        principalColumn: "seat_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TicketPurchaseSeat",
                columns: table => new
                {
                    purchase_id = table.Column<int>(nullable: false),
                    event_seat_id = table.Column<int>(nullable: false),
                    seat_subtotal = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TicketPu__B5CCA47E846C3728", x => new { x.event_seat_id, x.purchase_id });
                    table.ForeignKey(
                        name: "FK__TicketPur__event__4AB81AF0",
                        column: x => x.event_seat_id,
                        principalTable: "EventSeat",
                        principalColumn: "event_seat_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__TicketPur__purch__49C3F6B7",
                        column: x => x.purchase_id,
                        principalTable: "TicketPurchase",
                        principalColumn: "purchase_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_venue_name",
                table: "Event",
                column: "venue_name");

            migrationBuilder.CreateIndex(
                name: "IX_EventSeat_event_id",
                table: "EventSeat",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_EventSeat_seat_id",
                table: "EventSeat",
                column: "seat_id");

            migrationBuilder.CreateIndex(
                name: "IX_Row_section_id",
                table: "Row",
                column: "section_id");

            migrationBuilder.CreateIndex(
                name: "IX_Seat_row_id",
                table: "Seat",
                column: "row_id");

            migrationBuilder.CreateIndex(
                name: "IX_Section_venue_name",
                table: "Section",
                column: "venue_name");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPurchaseSeat_purchase_id",
                table: "TicketPurchaseSeat",
                column: "purchase_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPurchaseSeat");

            migrationBuilder.DropTable(
                name: "EventSeat");

            migrationBuilder.DropTable(
                name: "TicketPurchase");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Seat");

            migrationBuilder.DropTable(
                name: "Row");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropTable(
                name: "Venue");
        }
    }
}
