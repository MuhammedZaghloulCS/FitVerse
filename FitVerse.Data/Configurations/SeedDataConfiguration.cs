using FitVerse.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitVerse.Data.Configurations
{
    public class SeedDataConfiguration :
        IEntityTypeConfiguration<Anatomy>,
        IEntityTypeConfiguration<Equipment>,
        IEntityTypeConfiguration<Muscle>,
        IEntityTypeConfiguration<Exercise>
    {
        // 🧠 Anatomy
        public void Configure(EntityTypeBuilder<Anatomy> builder)
        {
            builder.HasData(
                new Anatomy { Id = 1, Name = "Chest", Image = "/images/anatomy/chest.png" },
                new Anatomy { Id = 2, Name = "Back", Image = "/images/anatomy/back.png" },
                new Anatomy { Id = 3, Name = "Legs", Image = "/images/anatomy/legs.png" },
                new Anatomy { Id = 4, Name = "Arms", Image = "/images/anatomy/arms.png" },
                new Anatomy { Id = 5, Name = "Shoulders", Image = "/images/anatomy/shoulders.png" }
            );
        }

        // 🏋️ Equipment
        void IEntityTypeConfiguration<Equipment>.Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasData(
                new Equipment { Id = 1, Name = "Barbell", Image = "/images/equipment/barbell.png" },
                new Equipment { Id = 2, Name = "Bodyweight", Image = "/images/equipment/bodyweight.png" },
                new Equipment { Id = 3, Name = "Dumbbell", Image = "/images/equipment/dumbbell.png" },
                new Equipment { Id = 4, Name = "Machine", Image = "/images/equipment/machine.png" },
                new Equipment { Id = 5, Name = "Cable", Image = "/images/equipment/cable.png" }
            );
        }

        // 💪 Muscles
        void IEntityTypeConfiguration<Muscle>.Configure(EntityTypeBuilder<Muscle> builder)
        {
            builder.HasData(
                new Muscle { Id = 1, Name = "Pectoralis Major", Description = "Main chest muscle responsible for pushing movements.", AnatomyId = 1 },
                new Muscle { Id = 2, Name = "Pectoralis Minor", Description = "Smaller chest muscle beneath pectoralis major.", AnatomyId = 1 },

                new Muscle { Id = 3, Name = "Latissimus Dorsi", Description = "Large back muscle used in pulling actions.", AnatomyId = 2 },
                new Muscle { Id = 4, Name = "Trapezius", Description = "Upper back and neck muscle responsible for posture.", AnatomyId = 2 },

                new Muscle { Id = 5, Name = "Quadriceps", Description = "Front thigh muscle responsible for leg extension.", AnatomyId = 3 },
                new Muscle { Id = 6, Name = "Biceps", Description = "Front upper arm muscle responsible for arm flexion.", AnatomyId = 4 },
                new Muscle { Id = 7, Name = "Deltoid", Description = "Main shoulder muscle responsible for arm rotation.", AnatomyId = 5 }
            );
        }

        // 🏋️‍♂️ Exercises
        void IEntityTypeConfiguration<Exercise>.Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasData(
                new Exercise { Id = 1, Name = "Bench Press", Description = "Classic chest exercise using a barbell.", VideoLink = "https://www.youtube.com/watch?v=rT7DgCr-3pg", MuscleId = 1, EquipmentId = 1 },
                new Exercise { Id = 2, Name = "Push Ups", Description = "Bodyweight exercise targeting the chest and triceps.", VideoLink = "https://www.youtube.com/watch?v=_l3ySVKYVJ8", MuscleId = 1, EquipmentId = 2 },

                new Exercise { Id = 3, Name = "Pull Ups", Description = "Upper back exercise using body weight.", VideoLink = "https://www.youtube.com/watch?v=eGo4IYlbE5g", MuscleId = 3, EquipmentId = 2 },
                new Exercise { Id = 4, Name = "Barbell Rows", Description = "Compound movement targeting the back.", VideoLink = "https://www.youtube.com/watch?v=vT2GjY_Umpw", MuscleId = 3, EquipmentId = 1 },

                new Exercise { Id = 5, Name = "Squats", Description = "Leg exercise working quads and glutes.", VideoLink = "https://www.youtube.com/watch?v=aclHkVaku9U", MuscleId = 5, EquipmentId = 1 },
                new Exercise { Id = 6, Name = "Bicep Curls", Description = "Isolated arm exercise for biceps.", VideoLink = "https://www.youtube.com/watch?v=ykJmrZ5v0Oo", MuscleId = 6, EquipmentId = 1 },
                new Exercise { Id = 7, Name = "Shoulder Press", Description = "Overhead press targeting the deltoid.", VideoLink = "https://www.youtube.com/watch?v=B-aVuyhvLHU", MuscleId = 7, EquipmentId = 1 }
            );
        }

    }

}
