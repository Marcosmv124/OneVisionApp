using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace One_Vision.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Historiales",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FECHA = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PACIENTE_ID = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Antecedente_Familiar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Antecedente_Personal = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Examenes_Complementarios = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tiempo_de_Uso = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Farmaco_Toxicológico = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Motivo_Sintomas = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AV_SC_OD_L = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_SC_OL_L = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_SC_OD_C = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_SC_OL_C = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_SC_OD_PH = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_SC_OL_PH = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_SC_AO_OD = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_SC_AO_OL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_L_OD_ESF = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_L_OL_ESF = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_L_OD_CIL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_L_OL_CIL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_L_OD_EJE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    AV_L_OL_EJE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MO_RP = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MO_LP = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MOV_RP = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MOV_LP = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MO_V_AO = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MO_SDL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MO_SDA = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MO_SEG = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_T = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_V = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_M1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_M2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_DIR1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_DIR2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_F1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_F2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_L1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_L_L2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_T = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_V = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_M1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_M2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_DIR1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_DIR2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_F1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_F2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_L1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CT_C_L2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CVPC_OD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CVPC_OI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RA_OD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RA_OI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    o_OD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    o_OI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    R_OD_ESF = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    R_OD_CIL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    R_OD_EJE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    R_OD_AV = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    R_OI_ESF = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    R_OI_CIL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    R_OI_EJE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    R_OI_AV = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    R_N = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    S_MP_OD_ESF = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_MP_OD_CIL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_MP_OD_EJE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_MP_OD_AV = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_MP_OI_ESF = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_MP_OI_CIL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_MP_OI_EJE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_MP_OI_AV = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_MP_N = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    S_CCJ_OD_ESF = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_CCJ_OD_CIL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_CCJ_OD_EJE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_CCJ_OD_AV = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_CCJ_OI_ESF = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_CCJ_OI_CIL = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_CCJ_OI_EJE = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_CCJ_OI_AV = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_CCJ_N = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    H_OD = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    H_OD_CORD = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    H_OI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    H_OI_CORD = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    VG_HL1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VG_HL2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VG_HC1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VG_HC2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VG_VL1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VG_VL2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VG_VC1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VG_VC2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    V_PPC_RUP = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    V_PPC_REC = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    A_MEM_OD = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    A_MEM_OI = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    A_CCF_OD = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    A_CCF_OI = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    S_LDW = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    DIAG = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PDM = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historiales", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Edad = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    CodigoDeBarra = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrecioDeVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioDeCompra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Existencia = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Proveedor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Moda = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Diseño = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.CodigoDeBarra);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rango = table.Column<int>(type: "int", nullable: false),
                    EmailConfirmado = table.Column<bool>(type: "bit", nullable: false),
                    TokenVerificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historiales");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
