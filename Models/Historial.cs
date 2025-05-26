using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace One_Vision.Models
{
    public class Historial
    {
       [Key]
        public int ID { get; set; }

        [StringLength(25)]
        public string? FECHA { get; set; }

        [Required]
        public int PacienteID { get; set; }

        [JsonIgnore]
        public virtual Paciente? Paciente { get; set; }

        [StringLength(200)]
        public string? Antecedente_Familiar { get; set; }

        [StringLength(200)]
        public string? Antecedente_Personal { get; set; }

        [StringLength(200)]
        public string? Examenes_Complementarios { get; set; }

        [StringLength(200)]
        public string? Tiempo_de_Uso { get; set; }

        [StringLength(200)]
        public string? Farmaco_Toxicológico { get; set; }

        [StringLength(200)]
        public string? Motivo_Sintomas { get; set; }

        // Campos visuales (fragmento actualizado con string nullable)
        [StringLength(25)] public string? AV_SC_OD_L { get; set; }
        [StringLength(25)] public string? AV_SC_OL_L { get; set; }
        [StringLength(25)] public string? AV_SC_OD_C { get; set; }
        [StringLength(25)] public string? AV_SC_OL_C { get; set; }
        [StringLength(25)] public string? AV_SC_OD_PH { get; set; }
        [StringLength(25)] public string? AV_SC_OL_PH { get; set; }
        [StringLength(25)] public string? AV_SC_AO_OD { get; set; }
        [StringLength(25)] public string? AV_SC_AO_OL { get; set; }
        [StringLength(25)] public string? AV_L_OD_ESF { get; set; }
        [StringLength(25)] public string? AV_L_OL_ESF { get; set; }
        [StringLength(25)] public string? AV_L_OD_CIL { get; set; }
        [StringLength(25)] public string? AV_L_OL_CIL { get; set; }
        [StringLength(25)] public string? AV_L_OD_EJE { get; set; }
        [StringLength(25)] public string? AV_L_OL_EJE { get; set; }
        [StringLength(25)] public string? AV_L_AO_L { get; set; }
        [StringLength(25)] public string? AV_L_AO_CIL { get; set; }
        [StringLength(25)] public string? AV_L_AO_EJE { get; set; }
        [StringLength(25)] public string? AV_L_OD_L { get; set; }
        [StringLength(25)] public string? AV_L_OL_L { get; set; }


        [StringLength(25)]
        public string? MO_RP { get; set; }

        [StringLength(25)]
        public string? MO_LP { get; set; }

        [StringLength(25)]
        public string? MOV_RP { get; set; }

        [StringLength(25)]
        public string? MOV_LP { get; set; }

        // Parte nueva (de tu segunda imagen)
        [StringLength(25)] public string? MO_V_AO { get; set; }
        [StringLength(25)] public string? MO_SDL { get; set; }
        [StringLength(25)] public string? MO_SDA { get; set; }
        [StringLength(25)] public string? MO_SEG { get; set; }
        [StringLength(25)] public string? CT_L_T { get; set; }
        [StringLength(25)] public string? CT_L_V { get; set; }
        [StringLength(25)] public string? CT_L_M1 { get; set; }
        [StringLength(25)] public string? CT_L_M2 { get; set; }
        [StringLength(25)] public string? CT_L_DIR1 { get; set; }
        [StringLength(25)] public string? CT_L_DIR2 { get; set; }
        [StringLength(25)] public string? CT_L_F1 { get; set; }
        [StringLength(25)] public string? CT_L_F2 { get; set; }
        [StringLength(25)] public string? CT_L_L1 { get; set; }
        [StringLength(25)] public string? CT_L_L2 { get; set; }
        [StringLength(25)] public string? CT_C_T { get; set; }
        [StringLength(25)] public string? CT_C_V { get; set; }
        [StringLength(25)] public string? CT_C_M1 { get; set; }
        [StringLength(25)] public string? CT_C_M2 { get; set; }
        [StringLength(25)] public string? CT_C_DIR1 { get; set; }
        [StringLength(25)] public string? CT_C_DIR2 { get; set; }
        [StringLength(25)] public string? CT_C_F1 { get; set; }
        [StringLength(25)] public string? CT_C_F2 { get; set; }
        [StringLength(25)] public string? CT_C_L1 { get; set; }
        [StringLength(25)] public string? CT_C_L2 { get; set; }

        [StringLength(100)] public string? CVPC_OD { get; set; }
        [StringLength(100)] public string? CVPC_OI { get; set; }

        [StringLength(100)] public string? RA_OD { get; set; }
        [StringLength(100)] public string? RA_OI { get; set; }

        [StringLength(100)] public string? o_OD { get; set; }
        [StringLength(100)] public string? o_OI { get; set; }

        [StringLength(25)] public string? R_OD_ESF { get; set; }
        [StringLength(25)] public string? R_OD_CIL { get; set; }
        [StringLength(25)] public string? R_OD_EJE { get; set; }
        [StringLength(25)] public string? R_OD_AV { get; set; }
        [StringLength(25)] public string? R_OI_ESF { get; set; }
        [StringLength(25)] public string? R_OI_CIL { get; set; }
        [StringLength(25)] public string? R_OI_EJE { get; set; }
        [StringLength(25)] public string? R_OI_AV { get; set; }

        [StringLength(100)] public string? R_N { get; set; }

        [StringLength(25)] public string? S_MP_OD_ESF { get; set; }
        [StringLength(25)] public string? S_MP_OD_CIL { get; set; }
        [StringLength(25)] public string? S_MP_OD_EJE { get; set; }
        [StringLength(25)] public string? S_MP_OD_AV { get; set; }
        [StringLength(25)] public string? S_MP_OI_ESF { get; set; }
        [StringLength(25)] public string? S_MP_OI_CIL { get; set; }
        [StringLength(25)] public string? S_MP_OI_EJE { get; set; }
        [StringLength(25)] public string? S_MP_OI_AV { get; set; }

        [StringLength(100)] public string? S_MP_N { get; set; }

        [StringLength(25)] public string? S_CCJ_OD_ESF { get; set; }
        [StringLength(25)] public string? S_CCJ_OD_CIL { get; set; }
        [StringLength(25)] public string? S_CCJ_OD_EJE { get; set; }
        [StringLength(25)] public string? S_CCJ_OD_AV { get; set; }
        [StringLength(25)] public string? S_CCJ_OI_ESF { get; set; }
        [StringLength(25)] public string? S_CCJ_OI_CIL { get; set; }
        [StringLength(25)] public string? S_CCJ_OI_EJE { get; set; }
        [StringLength(25)] public string? S_CCJ_OI_AV { get; set; }

        [StringLength(100)] public string? S_CCJ_N { get; set; }

        [StringLength(100)] public string? H_OD { get; set; }
        [StringLength(1000)] public string? H_OD_CORD { get; set; }
        [StringLength(100)] public string? H_OI { get; set; }
        [StringLength(1000)] public string? H_OI_CORD { get; set; }

        [StringLength(100)] public string? VG_HL1 { get; set; }
        [StringLength(100)] public string? VG_HL2 { get; set; }
        [StringLength(100)] public string? VG_HC1 { get; set; }
        [StringLength(100)] public string? VG_HC2 { get; set; }
        [StringLength(100)] public string? VG_VL1 { get; set; }
        [StringLength(100)] public string? VG_VL2 { get; set; }
        [StringLength(100)] public string? VG_VC1 { get; set; }
        [StringLength(100)] public string? VG_VC2 { get; set; }

        [StringLength(25)] public string? V_PPC_RUP { get; set; }
        [StringLength(25)] public string? V_PPC_REC { get; set; }

        [StringLength(25)] public string? A_MEM_OD { get; set; }
        [StringLength(25)] public string? A_MEM_OI { get; set; }

        [StringLength(25)] public string? A_CCF_OD { get; set; }
        [StringLength(25)] public string? A_CCF_OI { get; set; }
        [StringLength(25)] public string? S_LDW { get; set; }

        [StringLength(500)] public string? DIAG { get; set; }

        [StringLength(500)] public string? PDM { get; set; }

        [StringLength(2000)]
        public string? HallazgosOD { get; set; } // Ojo derecho

        [StringLength(2000)]
        public string? HallazgosOI { get; set; } // Ojo izquierdo

    }
}
