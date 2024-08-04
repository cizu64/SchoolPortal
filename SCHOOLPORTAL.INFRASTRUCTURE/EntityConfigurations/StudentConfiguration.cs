namespace Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities;

public class StudentConfiguration:IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        var navigation = builder.Metadata.FindNavigation(nameof(Student.StudentCourses));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        //builder.Ignore(d=>d.DomainEvents);

        builder.OwnsOne(s=>s.Address, a=>
        {
            a.WithOwner();

            a.Property(a=>a.City).HasColumnName("City").IsRequired();
            a.Property(a=>a.Street).HasColumnName("Street").IsRequired();
            a.Property(a=>a.Country).HasColumnName("Country").IsRequired();
            a.Property(a=>a.State).HasColumnName("State").HasMaxLength(60).IsRequired();
        });

        builder.Navigation(s=>s.Address).IsRequired();
    }
}