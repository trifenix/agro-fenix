﻿using System;
using System.ComponentModel.DataAnnotations;
using trifenix.connect.agro.index_model.enums;
using trifenix.connect.agro.index_model.props;
using trifenix.connect.agro.mdm_attributes;
using trifenix.connect.input;

namespace trifenix.connect.agro_model_input
{
    /// <summary>
    /// Documento que monitorea el traspaso o salida de productos desde o hacia la bodega
    /// </summary>
    [ReferenceSearchHeader(EntityRelated.WAREHOUSEDOCUMENT)]
    public class WarehouseDocumentInput : InputBase {

        /// <summary>
        /// Tipo de documento
        /// </summary>
        [Required]
        [EnumSearch(EnumRelated.DOCUMENT_TYPE)]
        public string Type { get; set; }

        /// <summary>
        /// Fecha de emisión del documento
        /// </summary>
        [Required]
        [DateSearch(DateRelated.EMISSION_DATE)]
        public DateTime EmissionDate { get; set; }

        /// <summary>
        /// Tipo de pago del documento
        /// </summary>
        [Required]
        [StringSearch(StringRelated.PAYMENT_TYPE)]
        public string PaymentType { get; set; }

        /// <summary>
        /// Define si es un documento de entrada o salida
        /// </summary>
        [Required]
        public bool Output { get; set; }

        /// <summary>
        /// Documento de producto sobre el cual realiza el documento de bodega
        /// </summary>
        [Required]
        [ReferenceSearch(EntityRelated.PRODUCTDOCUMENT, true)]
        public ProductDocumentInput[] ProductDocumentInput { get; set; }
    }
}