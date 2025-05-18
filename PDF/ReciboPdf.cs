using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using One_Vision.Models;

namespace One_Vision.PDF
{
    public class ReciboPdf : IDocument
    {
        public VentaReciboViewModel Datos { get; set; }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.Size(PageSizes.Letter);
                page.Content().Column(col =>
                {
                    // Header estilo AutoZone
                    col.Item().Row(row =>
                    {
                        row.RelativeColumn().Column(inner =>
                        {
                            inner.Item().Text("Óptica One Visión").Bold().FontSize(16);
                            inner.Item().Text("RFC: OVI-800101-XX1");
                            inner.Item().Text("Circuito Caracol 9043, Int. 302, Zona Río");
                            inner.Item().Text("Tijuana, BC, 22320");
                            inner.Item().Text("Tel: 664 683 2020");
                        });

                        row.ConstantColumn(150).Column(inner =>
                        {
                            inner.Item().Border(1).Padding(5).Background(Colors.Grey.Lighten3).Text("RECIBO").Bold().FontSize(14).AlignCenter();
                            inner.Item().PaddingTop(5).Text($"Folio: {Datos.ID_Venta}");
                            inner.Item().Text($"Fecha: {Datos.Fecha:dd/MM/yyyy}");
                            inner.Item().Text($"Paciente: {Datos.ID_Paciente}");
                        });
                    });

                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    // Encabezado tabla de productos
                    col.Item().Background(Colors.Grey.Lighten3).Padding(5).Row(header =>
                    {
                        header.RelativeColumn(4).Text("Descripción").Bold();
                        header.ConstantColumn(60).AlignRight().Text("Cant.").Bold();
                        header.ConstantColumn(80).AlignRight().Text("P. Unitario").Bold();
                        header.ConstantColumn(80).AlignRight().Text("Importe").Bold();
                    });

                    // Filas de productos
                    foreach (var p in Datos.Productos)
                    {
                        col.Item().Padding(5).Row(row =>
                        {
                            row.RelativeColumn(4).Text(p.Nombre);
                            row.ConstantColumn(60).AlignRight().Text($"{p.Cantidad}");
                            row.ConstantColumn(80).AlignRight().Text($"${p.Precio:0.00}");
                            row.ConstantColumn(80).AlignRight().Text($"${p.Precio * p.Cantidad:0.00}");
                        });
                    }

                    col.Item().LineHorizontal(1);

                    // Totales
                    col.Item().AlignRight().Column(totals =>
                    {
                        totals.Item().PaddingTop(5).Row(r =>
                        {
                            r.RelativeColumn().Text("Subtotal:");
                            r.ConstantColumn(100).AlignRight().Text($"${Datos.Total:0.00}");
                        });

                        totals.Item().Row(r =>
                        {
                            r.RelativeColumn().Text("Abonado:");
                            r.ConstantColumn(100).AlignRight().Text($"${Datos.Abonado:0.00}");
                        });

                        // Lógica para mostrar cambio o adeudo
                        if (Datos.Abonado >= Datos.Total)
                        {
                            var cambio = Datos.Abonado - Datos.Total;
                            totals.Item().Row(r =>
                            {
                                r.RelativeColumn().Text("Cambio:").Bold();
                                r.ConstantColumn(100).AlignRight().Text($"${cambio:0.00}").Bold();
                            });
                        }
                        else
                        {
                            var adeudo = Datos.Total - Datos.Abonado;
                            totals.Item().Row(r =>
                            {
                                r.RelativeColumn().Text("Adeudo:").Bold();
                                r.ConstantColumn(100).AlignRight().Text($"${adeudo:0.00}").Bold();
                            });
                        }
                    });


                    // Nota de agradecimiento
                    col.Item().PaddingTop(15).Text("Gracias por su compra. ¡Vuelva pronto!").Italic().AlignCenter();
                });
            });
        }
    }
}
