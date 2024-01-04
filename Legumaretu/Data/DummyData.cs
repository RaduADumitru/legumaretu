using Legumaretu.Models;

namespace Legumaretu.Data
{
	public class DummyData
	{
		static string LoremShort { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nibh mauris cursus mattis molestie a. Amet nulla facilisi morbi tempus.";
		static string LoremIpsum { get; set; } = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Nibh mauris cursus mattis molestie a. Amet nulla facilisi morbi tempus. Suspendisse in est ante in nibh mauris cursus. Pellentesque adipiscing commodo elit at imperdiet. Euismod elementum nisi quis eleifend quam. Condimentum id venenatis a condimentum vitae sapien pellentesque habitant. Dui id ornare arcu odio ut sem nulla pharetra. Egestas fringilla phasellus faucibus scelerisque eleifend donec pretium. In egestas erat imperdiet sed euismod. Lacus suspendisse faucibus interdum posuere lorem. Pharetra pharetra massa massa ultricies. Elit scelerisque mauris pellentesque pulvinar pellentesque. Faucibus nisl tincidunt eget nullam non nisi est sit amet. Commodo viverra maecenas accumsan lacus vel facilisis volutpat. Quisque egestas diam in arcu cursus euismod quis viverra nibh. Elementum integer enim neque volutpat ac tincidunt. At auctor urna nunc id. Eget mi proin sed libero enim sed faucibus turpis. Egestas integer eget aliquet nibh praesent tristique magna. In arcu cursus euismod quis viverra nibh cras pulvinar mattis. Lectus magna fringilla urna porttitor rhoncus. Nibh cras pulvinar mattis nunc sed blandit libero volutpat. Rhoncus dolor purus non enim praesent elementum. Scelerisque eleifend donec pretium vulputate sapien nec sagittis aliquam malesuada. Convallis a cras semper auctor neque vitae tempus quam pellentesque. At elementum eu facilisis sed odio morbi quis commodo odio. Et leo duis ut diam quam nulla porttitor massa. Aliquam nulla facilisi cras fermentum odio eu. Purus viverra accumsan in nisl nisi scelerisque eu. Magna fermentum iaculis eu non diam phasellus vestibulum lorem. Egestas integer eget aliquet nibh praesent tristique magna. Eget velit aliquet sagittis id consectetur purus ut faucibus pulvinar. Phasellus vestibulum lorem sed risus ultricies tristique nulla. Libero nunc consequat interdum varius sit amet mattis. Porttitor massa id neque aliquam vestibulum morbi blandit. Massa tempor nec feugiat nisl pretium fusce. Egestas dui id ornare arcu odio ut. Sed risus ultricies tristique nulla aliquet enim tortor at auctor. Faucibus in ornare quam viverra orci sagittis eu volutpat. Dignissim diam quis enim lobortis scelerisque. Pharetra massa massa ultricies mi. Lobortis mattis aliquam faucibus purus in massa tempor nec. Ac placerat vestibulum lectus mauris ultrices eros in cursus turpis. Sit amet tellus cras adipiscing enim eu turpis egestas pretium. Leo a diam sollicitudin tempor id. Et malesuada fames ac turpis egestas sed. Gravida cum sociis natoque penatibus et magnis dis. Id diam maecenas ultricies mi eget mauris pharetra. Luctus accumsan tortor posuere ac ut consequat semper. At erat pellentesque adipiscing commodo elit at imperdiet. Non consectetur a erat nam at lectus urna duis convallis. Pellentesque eu tincidunt tortor aliquam nulla facilisi cras fermentum odio. Elementum facilisis leo vel fringilla est ullamcorper eget. Aliquam malesuada bibendum arcu vitae elementum curabitur. Sed tempus urna et pharetra pharetra massa massa. Velit sed ullamcorper morbi tincidunt ornare massa eget egestas purus. Viverra orci sagittis eu volutpat odio facilisis mauris sit amet. Mi tempus imperdiet nulla malesuada. Urna porttitor rhoncus dolor purus non enim praesent elementum.";

		public static List<Recipe> TestRecipes { get; set; } = new()
		{
			new Recipe(1, "Recipe1", LoremIpsum, 3, false, "/content/images/recipes/1280px-Sunflower_from_Silesia2.jpg"),
			new Recipe(2, "Recipe2", LoremIpsum, 1, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg"),
			new Recipe(3, "Recipe3", LoremIpsum, 4, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg"),
			new Recipe(4, "Recipe4", LoremIpsum, 5, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg")
		};

		public static List<Challenge> TestChallenges { get; set; } = new()
		{
			new Challenge(1, "Challenge1", LoremShort, true, new List<Recipe> ()
			{
				new Recipe(5, "Recipe5", LoremIpsum, 4, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg"),
				new Recipe(6, "Recipe6", LoremIpsum, 3, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg"),
				new Recipe(7, "Recipe7", LoremIpsum, 2, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg"),
				new Recipe(8, "Recipe8", LoremIpsum, 5, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg")
			}),
			new Challenge(2, "Challenge2", LoremShort, true, new List<Recipe> ()
			{
				new Recipe(9, "Recipe9", LoremIpsum, 2, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg"),
				new Recipe(10, "Recipe10", LoremIpsum, 4, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg"),
				new Recipe(11, "Recipe11", LoremIpsum, 5, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg"),
				new Recipe(12, "Recipe12", LoremIpsum, 2, true, "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg")
			})
		};
	}
}
