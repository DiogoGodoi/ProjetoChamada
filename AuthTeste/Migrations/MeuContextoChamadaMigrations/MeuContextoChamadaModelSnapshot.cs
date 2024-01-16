﻿// <auto-generated />
using AuthTeste.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthTeste.Migrations.MeuContextoChamadaMigrations
{
    [DbContext(typeof(MeuContextoChamada))]
    partial class MeuContextoChamadaModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AuthTeste.Models.MdlEscola", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("UrlImage")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.HasIndex("UrlImage")
                        .IsUnique()
                        .HasFilter("[UrlImage] IS NOT NULL");

                    b.ToTable("Escola");
                });

            modelBuilder.Entity("AuthTeste.Models.MdlEscolaProfessor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Fk_Escola_Id")
                        .HasColumnType("int");

                    b.Property<int>("Fk_Professor_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Fk_Escola_Id");

                    b.HasIndex("Fk_Professor_Id");

                    b.ToTable("Escola_Professor");
                });

            modelBuilder.Entity("AuthTeste.Models.MdlPais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Cidade")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Cpf")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Estado")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Logradouro")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasFilter("[Cpf] IS NOT NULL");

                    b.ToTable("Pais");
                });

            modelBuilder.Entity("AuthTeste.Models.MdlProfessor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Cpf")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("Cref")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasFilter("[Cpf] IS NOT NULL");

                    b.HasIndex("Cref")
                        .IsUnique();

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("AuthTeste.Models.MdlProfessorTurma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Fk_Professor_Id")
                        .HasColumnType("int");

                    b.Property<int>("Fk_Turma_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Fk_Professor_Id");

                    b.HasIndex("Fk_Turma_Id");

                    b.ToTable("Professor_Turma");
                });

            modelBuilder.Entity("AuthTeste.Models.MdlTurma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Fk_Escola_Id")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Fk_Escola_Id");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("MdlProfessorMdlTurma", b =>
                {
                    b.Property<int>("ProfessoresId")
                        .HasColumnType("int");

                    b.Property<int>("TurmasId")
                        .HasColumnType("int");

                    b.HasKey("ProfessoresId", "TurmasId");

                    b.HasIndex("TurmasId");

                    b.ToTable("MdlProfessorMdlTurma");
                });

            modelBuilder.Entity("AuthTeste.Models.MdlEscolaProfessor", b =>
                {
                    b.HasOne("AuthTeste.Models.MdlEscola", "Escola")
                        .WithMany()
                        .HasForeignKey("Fk_Escola_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthTeste.Models.MdlProfessor", "Professor")
                        .WithMany()
                        .HasForeignKey("Fk_Professor_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("AuthTeste.Models.MdlProfessorTurma", b =>
                {
                    b.HasOne("AuthTeste.Models.MdlProfessor", "Professor")
                        .WithMany()
                        .HasForeignKey("Fk_Professor_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthTeste.Models.MdlTurma", "Turma")
                        .WithMany()
                        .HasForeignKey("Fk_Turma_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("AuthTeste.Models.MdlTurma", b =>
                {
                    b.HasOne("AuthTeste.Models.MdlEscola", "Escola")
                        .WithMany("Turmas")
                        .HasForeignKey("Fk_Escola_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escola");
                });

            modelBuilder.Entity("MdlProfessorMdlTurma", b =>
                {
                    b.HasOne("AuthTeste.Models.MdlProfessor", null)
                        .WithMany()
                        .HasForeignKey("ProfessoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AuthTeste.Models.MdlTurma", null)
                        .WithMany()
                        .HasForeignKey("TurmasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AuthTeste.Models.MdlEscola", b =>
                {
                    b.Navigation("Turmas");
                });
#pragma warning restore 612, 618
        }
    }
}
