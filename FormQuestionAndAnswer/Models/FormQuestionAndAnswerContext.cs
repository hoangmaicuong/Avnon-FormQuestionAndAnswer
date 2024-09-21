using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FormQuestionAndAnswer.Models;

public partial class FormQuestionAndAnswerContext : DbContext
{
    public FormQuestionAndAnswerContext()
    {
    }

    public FormQuestionAndAnswerContext(DbContextOptions<FormQuestionAndAnswerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<AnswerOption> AnswerOptions { get; set; }

    public virtual DbSet<AnswerType> AnswerTypes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionTitle> QuestionTitles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MAICUONG-LAP;Initial Catalog=FormQuestionAndAnswer;User ID=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnswerOptionId).HasColumnName("answer_option_id");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.AnswerOption).WithMany(p => p.Answers)
                .HasForeignKey(d => d.AnswerOptionId)
                .HasConstraintName("FK_Answer_AnswerOption");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Answer_Questions");
        });

        modelBuilder.Entity<AnswerOption>(entity =>
        {
            entity.ToTable("AnswerOption");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OptionAnswerContent)
                .HasMaxLength(225)
                .HasColumnName("option_answer_content");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");

            entity.HasOne(d => d.Question).WithMany(p => p.AnswerOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_AnswerOption_Questions");
        });

        modelBuilder.Entity<AnswerType>(entity =>
        {
            entity.ToTable("AnswerType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(204)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnswerContent)
                .HasMaxLength(225)
                .HasColumnName("answer_content");
            entity.Property(e => e.AnswerTypeId).HasColumnName("answer_type_id");
            entity.Property(e => e.IsRequired).HasColumnName("is_required");
            entity.Property(e => e.QuestionContent)
                .HasMaxLength(500)
                .HasColumnName("question_content");
            entity.Property(e => e.QuestionTitleId).HasColumnName("question_title_id");

            entity.HasOne(d => d.AnswerType).WithMany(p => p.Questions)
                .HasForeignKey(d => d.AnswerTypeId)
                .HasConstraintName("FK_Questions_AnswerType");

            entity.HasOne(d => d.QuestionTitle).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuestionTitleId)
                .HasConstraintName("FK_Questions_QuestionTitle");
        });

        modelBuilder.Entity<QuestionTitle>(entity =>
        {
            entity.ToTable("QuestionTitle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QuestionTitleContent)
                .HasMaxLength(225)
                .HasColumnName("question_title_content");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
