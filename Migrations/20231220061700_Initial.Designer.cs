﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using liberacao_credito.Context;

#nullable disable

namespace liberacao_credito.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231220061700_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("liberacao_credito.Models.Cliente", b =>
                {
                    b.Property<string>("CPF")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(11)");

                    b.Property<string>("UF")
                        .IsRequired()
                        .HasColumnType("char(2)");

                    b.HasKey("CPF");

                    b.ToTable("Cliente", (string)null);

                    b.HasData(
                        new
                        {
                            CPF = "123.456.789-09",
                            Nome = "Cliente1",
                            Telefone = "11987654000",
                            UF = "SP"
                        },
                        new
                        {
                            CPF = "987.654.321-09",
                            Nome = "Cliente2",
                            Telefone = "21987655000",
                            UF = "RJ"
                        },
                        new
                        {
                            CPF = "111.222.333-44",
                            Nome = "Cliente3",
                            Telefone = "11987656000",
                            UF = "SP"
                        },
                        new
                        {
                            CPF = "555.666.777-88",
                            Nome = "Cliente4",
                            Telefone = "11987657000",
                            UF = "SP"
                        },
                        new
                        {
                            CPF = "999.888.777-66",
                            Nome = "Cliente5",
                            Telefone = "21987657770",
                            UF = "RJ"
                        });
                });

            modelBuilder.Entity("liberacao_credito.Models.Financiamento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataVencimentoUltimo")
                        .HasColumnType("datetime");

                    b.Property<int>("TipoCreditoId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("numeric(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("CPF");

                    b.HasIndex("TipoCreditoId");

                    b.ToTable("Financiamento", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CPF = "123.456.789-09",
                            DataVencimentoUltimo = new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipoCreditoId = 1,
                            ValorTotal = 5500m
                        },
                        new
                        {
                            Id = 2L,
                            CPF = "123.456.789-09",
                            DataVencimentoUltimo = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipoCreditoId = 2,
                            ValorTotal = 75000m
                        },
                        new
                        {
                            Id = 3L,
                            CPF = "111.222.333-44",
                            DataVencimentoUltimo = new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipoCreditoId = 3,
                            ValorTotal = 187500m
                        },
                        new
                        {
                            Id = 4L,
                            CPF = "999.888.777-66",
                            DataVencimentoUltimo = new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipoCreditoId = 4,
                            ValorTotal = 7500m
                        },
                        new
                        {
                            Id = 5L,
                            CPF = "999.888.777-66",
                            DataVencimentoUltimo = new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipoCreditoId = 4,
                            ValorTotal = 10000m
                        },
                        new
                        {
                            Id = 6L,
                            CPF = "987.654.321-09",
                            DataVencimentoUltimo = new DateTime(2021, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipoCreditoId = 5,
                            ValorTotal = 50000m
                        },
                        new
                        {
                            Id = 7L,
                            CPF = "987.654.321-09",
                            DataVencimentoUltimo = new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TipoCreditoId = 5,
                            ValorTotal = 30000m
                        });
                });

            modelBuilder.Entity("liberacao_credito.Models.Parcela", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("FinanciamentoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DataPagamento")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("datetime");

                    b.Property<byte>("NumParcela")
                        .HasColumnType("tinyint");

                    b.Property<decimal>("ValorParcela")
                        .HasColumnType("numeric(9,2)");

                    b.HasKey("Id", "FinanciamentoId");

                    b.HasIndex("FinanciamentoId");

                    b.ToTable("Parcela", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            FinanciamentoId = 1L,
                            DataPagamento = new DateTime(2022, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)1,
                            ValorParcela = 1000m
                        },
                        new
                        {
                            Id = 2L,
                            FinanciamentoId = 1L,
                            DataPagamento = new DateTime(2022, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)2,
                            ValorParcela = 1200m
                        },
                        new
                        {
                            Id = 3L,
                            FinanciamentoId = 1L,
                            DataPagamento = new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)3,
                            ValorParcela = 800m
                        },
                        new
                        {
                            Id = 4L,
                            FinanciamentoId = 1L,
                            DataVencimento = new DateTime(2022, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)4,
                            ValorParcela = 1000m
                        },
                        new
                        {
                            Id = 5L,
                            FinanciamentoId = 1L,
                            DataVencimento = new DateTime(2022, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)5,
                            ValorParcela = 1500m
                        },
                        new
                        {
                            Id = 6L,
                            FinanciamentoId = 2L,
                            DataPagamento = new DateTime(2023, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)1,
                            ValorParcela = 15000m
                        },
                        new
                        {
                            Id = 7L,
                            FinanciamentoId = 2L,
                            DataPagamento = new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)2,
                            ValorParcela = 15000m
                        },
                        new
                        {
                            Id = 8L,
                            FinanciamentoId = 2L,
                            DataVencimento = new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)3,
                            ValorParcela = 15000m
                        },
                        new
                        {
                            Id = 9L,
                            FinanciamentoId = 2L,
                            DataVencimento = new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)4,
                            ValorParcela = 15000m
                        },
                        new
                        {
                            Id = 10L,
                            FinanciamentoId = 2L,
                            DataVencimento = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)5,
                            ValorParcela = 15000m
                        },
                        new
                        {
                            Id = 11L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)1,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 12L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2022, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)2,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 13L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2022, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)3,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 14L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2022, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)4,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 15L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2022, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)5,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 16L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2022, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)6,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 17L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)7,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 18L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)8,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 19L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2023, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)9,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 20L,
                            FinanciamentoId = 3L,
                            DataPagamento = new DateTime(2023, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)10,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 21L,
                            FinanciamentoId = 3L,
                            DataVencimento = new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)11,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 22L,
                            FinanciamentoId = 3L,
                            DataVencimento = new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)12,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 23L,
                            FinanciamentoId = 3L,
                            DataVencimento = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)13,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 24L,
                            FinanciamentoId = 3L,
                            DataVencimento = new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)14,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 25L,
                            FinanciamentoId = 3L,
                            DataVencimento = new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)15,
                            ValorParcela = 12500m
                        },
                        new
                        {
                            Id = 26L,
                            FinanciamentoId = 4L,
                            DataPagamento = new DateTime(2022, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)1,
                            ValorParcela = 500m
                        },
                        new
                        {
                            Id = 27L,
                            FinanciamentoId = 4L,
                            DataPagamento = new DateTime(2022, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)2,
                            ValorParcela = 700m
                        },
                        new
                        {
                            Id = 28L,
                            FinanciamentoId = 4L,
                            DataPagamento = new DateTime(2022, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)3,
                            ValorParcela = 1000m
                        },
                        new
                        {
                            Id = 29L,
                            FinanciamentoId = 4L,
                            DataPagamento = new DateTime(2022, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2022, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)4,
                            ValorParcela = 2000m
                        },
                        new
                        {
                            Id = 30L,
                            FinanciamentoId = 4L,
                            DataVencimento = new DateTime(2022, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)5,
                            ValorParcela = 3300m
                        },
                        new
                        {
                            Id = 31L,
                            FinanciamentoId = 5L,
                            DataPagamento = new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)1,
                            ValorParcela = 1000m
                        },
                        new
                        {
                            Id = 32L,
                            FinanciamentoId = 5L,
                            DataPagamento = new DateTime(2023, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)2,
                            ValorParcela = 1000m
                        },
                        new
                        {
                            Id = 33L,
                            FinanciamentoId = 5L,
                            DataPagamento = new DateTime(2023, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)3,
                            ValorParcela = 1000m
                        },
                        new
                        {
                            Id = 34L,
                            FinanciamentoId = 5L,
                            DataPagamento = new DateTime(2023, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)4,
                            ValorParcela = 1000m
                        },
                        new
                        {
                            Id = 35L,
                            FinanciamentoId = 5L,
                            DataVencimento = new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)5,
                            ValorParcela = 2000m
                        },
                        new
                        {
                            Id = 36L,
                            FinanciamentoId = 5L,
                            DataVencimento = new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)6,
                            ValorParcela = 4000m
                        },
                        new
                        {
                            Id = 37L,
                            FinanciamentoId = 6L,
                            DataPagamento = new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2021, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)1,
                            ValorParcela = 10000m
                        },
                        new
                        {
                            Id = 38L,
                            FinanciamentoId = 6L,
                            DataPagamento = new DateTime(2021, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2021, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)2,
                            ValorParcela = 8000m
                        },
                        new
                        {
                            Id = 39L,
                            FinanciamentoId = 6L,
                            DataPagamento = new DateTime(2021, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2021, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)3,
                            ValorParcela = 8000m
                        },
                        new
                        {
                            Id = 40L,
                            FinanciamentoId = 6L,
                            DataPagamento = new DateTime(2021, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2021, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)4,
                            ValorParcela = 8000m
                        },
                        new
                        {
                            Id = 41L,
                            FinanciamentoId = 6L,
                            DataPagamento = new DateTime(2021, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2021, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)5,
                            ValorParcela = 6000m
                        },
                        new
                        {
                            Id = 42L,
                            FinanciamentoId = 6L,
                            DataPagamento = new DateTime(2021, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2021, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)6,
                            ValorParcela = 10000m
                        },
                        new
                        {
                            Id = 43L,
                            FinanciamentoId = 7L,
                            DataPagamento = new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)1,
                            ValorParcela = 6000m
                        },
                        new
                        {
                            Id = 44L,
                            FinanciamentoId = 7L,
                            DataPagamento = new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)2,
                            ValorParcela = 3000m
                        },
                        new
                        {
                            Id = 45L,
                            FinanciamentoId = 7L,
                            DataPagamento = new DateTime(2023, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)3,
                            ValorParcela = 6000m
                        },
                        new
                        {
                            Id = 46L,
                            FinanciamentoId = 7L,
                            DataPagamento = new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)4,
                            ValorParcela = 6000m
                        },
                        new
                        {
                            Id = 47L,
                            FinanciamentoId = 7L,
                            DataPagamento = new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DataVencimento = new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            NumParcela = (byte)5,
                            ValorParcela = 9000m
                        });
                });

            modelBuilder.Entity("liberacao_credito.Models.TipoCredito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<bool>("IsAtivo")
                        .HasColumnType("bit");

                    b.Property<decimal>("Taxa")
                        .HasColumnType("numeric(3,2)");

                    b.HasKey("Id");

                    b.ToTable("TipoCredito", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descricao = "Direto",
                            IsAtivo = true,
                            Taxa = 2m
                        },
                        new
                        {
                            Id = 2,
                            Descricao = "Consignado",
                            IsAtivo = true,
                            Taxa = 1m
                        },
                        new
                        {
                            Id = 3,
                            Descricao = "Pessoa Jurídica",
                            IsAtivo = true,
                            Taxa = 5m
                        },
                        new
                        {
                            Id = 4,
                            Descricao = "Pessoa Física",
                            IsAtivo = true,
                            Taxa = 3m
                        },
                        new
                        {
                            Id = 5,
                            Descricao = "Imobiliário",
                            IsAtivo = true,
                            Taxa = 9m
                        });
                });

            modelBuilder.Entity("liberacao_credito.Models.Financiamento", b =>
                {
                    b.HasOne("liberacao_credito.Models.Cliente", "Cliente")
                        .WithMany("Financiamento")
                        .HasForeignKey("CPF")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("liberacao_credito.Models.TipoCredito", "TipoCredito")
                        .WithMany()
                        .HasForeignKey("TipoCreditoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("TipoCredito");
                });

            modelBuilder.Entity("liberacao_credito.Models.Parcela", b =>
                {
                    b.HasOne("liberacao_credito.Models.Financiamento", "Financiamento")
                        .WithMany("Parcela")
                        .HasForeignKey("FinanciamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Financiamento");
                });

            modelBuilder.Entity("liberacao_credito.Models.Cliente", b =>
                {
                    b.Navigation("Financiamento");
                });

            modelBuilder.Entity("liberacao_credito.Models.Financiamento", b =>
                {
                    b.Navigation("Parcela");
                });
#pragma warning restore 612, 618
        }
    }
}
