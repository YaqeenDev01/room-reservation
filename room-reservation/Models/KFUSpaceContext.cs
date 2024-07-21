using Microsoft.EntityFrameworkCore;

namespace  room_reservation.Models
{
    public class KFUSpaceContext : DbContext
    {
        public DbSet<tblRoles> tblRoles { get; set; }
        public DbSet<tblPermissions> tblPermissions { get; set; }
        public DbSet<tblRooms> tblRooms { get; set; }
        public DbSet<tblRoomType> tblRoomType { get; set; }
        public DbSet<tblBookings> tblBookings { get; set; }
        public DbSet<tblBookingStatues> tblBookingStatues { get; set; }
        public DbSet<tblBuildings> tblBuildings { get; set; }
        public DbSet<tblFloors> tblFloors { get; set; }
        public DbSet<tblLectures> tblLectures { get; set; }
        public DbSet<tblUsers>  tblUsers { get; set; }
        public DbSet<FloorsLog> FloorsLog { get; set; }
        public DbSet<BuildingsLog> BuildingsLog { get; set; }
        public DbSet<PermissionsLog> PermissionsLog { get; set; }
        public DbSet<RoomsLog> RoomsLog { get; set; }
        public DbSet<BookingsLog> BookingsLog { get; set; }
        
        public KFUSpaceContext()
        {

        }

        public KFUSpaceContext(DbContextOptions<KFUSpaceContext> options)
                   : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBCS"));
        }
    }


}
