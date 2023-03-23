using System.Globalization;

namespace server.Core.Services;


public class FoodItemSeeding
{
    private readonly IFoodItemRepository _repo;

    public FoodItemSeeding(IFoodItemRepository repo)
    {
        _repo = repo;
    }
    public async Task<Response> Seed()
    {
        CultureInfo.CurrentCulture = new CultureInfo("da-DK", false);
        using(var reader = new StreamReader(@"../data.csv"))
        {
            List<FoodItemCreateDTO> foodItems = new List<FoodItemCreateDTO>();
            reader.ReadLine();//Skip first line: Danish field declaration
            reader.ReadLine();//Skip second line: Engligsh field declaration
            reader.ReadLine();//Skip third line: Unit declaration
            reader.ReadLine();//Skip fourth line: IDs

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine()!;
                string[] values = line.Split(';');
                
                FoodItemCreateDTO foodItem = new FoodItemCreateDTO
                {
                    Name = values[1],
                    Calories = float.Parse(values[4] != "" ? values[4] : "0"), // kcal/100g
                    Protein = float.Parse(values[5] != "" ? values[5] : "0"), //g/100g
                    Carbohydrates = float.Parse(values[10] != "" ? values[10] : "0"), //Carbohydrate by labelling g/100g
                    Sugars = float.Parse(values[86] != "" ? values[86] : "0"), //g/100g
                    Fibres = float.Parse(values[11] != "" ? values[11] : "0"), //g/100g
                    TotalFat = float.Parse(values[171] != "" ? values[171] : "0"),//g/100g
                    SaturatedFat = float.Parse(values[166] != "" ? values[166] : "0"), //g/100g
                    MonounsaturatedFat = float.Parse(values[167] != "" ? values[167] : "0"), //g/100g
                    PolyunsaturatedFat = float.Parse(values[168] != "" ? values[168] : "0"), //g/100g
                    TransFat = float.Parse(values[169] != "" ? values[169] : "0"), //g/100 g
                    VitaminA = float.Parse(values[18] != "" ? values[18] : "0"), //RE (µg/100g)
                    VitaminB6 = float.Parse(values[39] != "" ? values[39] : "0"), //mg/100g
                    VitaminB12 = float.Parse(values[44] != "" ? values[44] : "0"), //µg/100g
                    VitaminC = float.Parse(values[45] != "" ? values[45] : "0"), //mg/100g
                    VitaminD = float.Parse(values[21] != "" ? values[21] : "0"), //µg/100g
                    VitaminE = float.Parse(values[26] != "" ? values[26] : "0"), // alfa-TE/100g eller mg/100g
                    Thiamin = float.Parse(values[33] != "" ? values[33] : "0"), //mg/100g
                    Riboflavin = float.Parse(values[36] != "" ? values[36] : "0"),//mg/100g
                    Niacin = float.Parse(values[38] != "" ? values[38] : "0"), //mg/100g
                    Folate = float.Parse(values[42] != "" ? values[42] : "0"), // µg/100g
                    Salt = float.Parse(values[14] != "" ? values[14] : "0"), //g/100g
                    Potassium = float.Parse(values[49] != "" ? values[49] : "0"), //mg/100g
                    Magnesium = float.Parse(values[51] != "" ? values[51] : "0"), //mg/100g
                    Iron = float.Parse(values[53] != "" ? values[53] : "0"), //mg/100g
                    Zinc = float.Parse(values[55] != "" ? values[55] : "0"), //mg/100g
                    Phosphorus = float.Parse(values[64] != "" ? values[64] : "0"), //mg/100g
                    Copper = float.Parse(values[54] != "" ? values[54] : "0"), //mg/100g
                    Iodine = float.Parse(values[68] != "" ? values[68] : "0"), //µg/100g
                    Selenium = float.Parse(values[60] != "" ? values[60] : "0"), //µg/100g
                    Calcium = float.Parse(values[50] != "" ? values[50] : "0"), //mg/100g
                };
                foodItems.Add(foodItem);
            }
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            foreach(var foodItem in foodItems)
            {
                await _repo.CreateAsync(foodItem);
            }
            return Response.Created;
        }
    }
/*
0: 
1: 
2: ?ParameterNavn
3: Energi (kJ)
4: Energi (kcal)
5: Protein
6: Protein fra Aminosyrer
7: Protein, 8deklaration
8: Kulhydrat difference
9: Tilg�ngelig kulhydrat
10: Tilg�ngelig kulhydrat, deklaration
11: Kostfibre
12: Fedt
13: Alkohol
14: Salt deklaration
15: Aske
16: T�rstof
17: Vand
18: A-vitamin
19: Retinol
20: beta-Caroten
21: D-vitamin
22: D3-vitamin
23: D2-vitamin
24:  25-hydroxy D3-vitamin
25:  25-hydroxy D2-vitamin
26: E-vitamin
27: alfa-Tokoferol
28: beta-Tokoferol
29: gamma-Tokoferol
30: delta-Tokoferol
31: alfa-Tokotrienol
33: Thiamin (B1-vitamin)
34: Thiamin
35: 2-(1-hydroxyethyl)thiamin
36: Riboflavin (B2-vitamin)
37: Niacin�kvivalent
38: Niacin
39: B6-vitamin
40: Pantothensyre
41: Biotin
42: Folat
43: Frit folat
44: B12-vitamin
45: C-vitamin
46: Ascorbinsyre
47: Dehydroascorbinsyre
48: Natrium
49: Kalium
50: Calcium
51: Magnesium
52: Rubidium
53: Jern
54: Kobber
55: Zink
56: Mangan
57: Krom
58: Molybd�n
59: Cobolt
60: Selen
61: Aluminium
62: Silicium
63: Bor
64: Fosfor
65: Fluor
66: Chlorid
67: Brom
68: Jod
69: Svovl
70: Nikkel
71: Kviks�lv
72: Arsen
73: Cadmium
74: Bly
75: Tin
76: Fruktose
77: Galaktose
78: Glukose
79: Sum monosakkarider
80: Laktose
81: Maltose
82: Sakkarose
83: Sum disakkarider
84: Maltotriose
85: Raffinose
86: Sum sukkerarter
87: Tilsat Sukker
88: Frie sukkerarter
89: Sorbitol
90: Mannitol
91: Inositol
92: Maltitol
93: Sum sukkeralkoholer
94: Stivelse
95: Cellulose
96: Lignin
97: Uopl�selige kostfibre
98: H�j molekylev�gt opl�selige kostfibre
99: Lav molekylev�gt opl�selige kostfibre
100: Crude fibre
101: Neutral detergent fibre
102: L-m�lkesyre
103: D-m�lkesyre
104: M�lkesyre
105: Citronsyre
106: Oxalsyre
107: �blesyre
108: Eddikesyre
109: Propionsyre
110: Benzosyre
111: Sum organiske syrer
112: C4:0
113: C6:0
114: C8:0
115: C10:0
116: C12:0
117: C13:0
118: C14:0
119: C15:0
120: C16:0
121: C17:0
122: C18:0
123: C20:0
124: C21:0
125: C22:0
126: C23:0
127: C24:0
128: C12:1,n-1
129: C14:1,n-5
130: C15:1,n-5
131: C16:1,n-7
132: C17:1,n-7
133: C18:1,n-7
134: C18:1,n-9
135: C18:1,n-12
136: C20:1,n-9
137: C20:1,n-11
138: C20:1,n-15
139: C22:1,n-9
140: C22:1,n-11
141: C24:1,n-9
142: C14:1,n-5,trans
143: C16:1,n-7,trans
144: C18:1,trans
145: C20:1,trans
146: C22:1,trans
147: C18:2,n-6
148: C18:2,konjugeret
149: C18:3,n-3
150: C18:3,n-6
151: C18:4,n-3
152: C20:2,n-6
153: C20:3,n-3
154: C20:3,n-6
155: C20:4,n-3
156: C20:4,n-6
157: C20:5,n-3
158: C22:2,n-6
159: C22:3,n-3
160: C22:4,n-6
161: C22:5,n-3
162: C22:5,n-6
163: C22:6,n-3
164: C18:2,trans
165: Andre fedtsyrer
166: Sum m�ttede fedtsyrer
167: Sum enkeltum�ttede fedtsyrer
168: Sum flerum�ttede fedtsyrer
169: Sum transfedtsyrer
170: Sum fedtsyrer under detektionsgr�nsen
171: Sum fedtsyrer
172: Sum n-3 fedtsyrer
173: Sum n-6 fedtsyrer
174: Kolesterol
175: Nitrogen
176: Isoleucin
177: Leucin
178: Lysin
179: Methionin
180: Cystin
181: Phenylalanin
182: Tyrosin
183: Threonin
184: Tryptofan
185: Valin
186: Arginin
187: Histidin
188: Alanin
189: Asparaginsyre
190: Glutaminsyre
191: Glycin
192: Prolin
193: Serin
194: Hydroxyprolin
195: Ornitin
196: Histamin
197: Serotonin
198: Tyramin
199: Phenylethylamin
200: Putrescin
201: Cadaverin
202: Spermin
203: Spermidin
204: Sum biogene aminer
205: Svind
206: Nitrogen-til-protein faktor
207: Fedtsyrekonverteringsfaktor
208: Massefylde
    */





    public static void Names()
    {
        using(var reader = new StreamReader(@"../../../../data.csv"))
        {
            List<FoodItemCreateDTO> foodItems = new List<FoodItemCreateDTO>();
            string line = reader.ReadLine()!;
            var names = line.Split(";");
            for (int i = 0; i<names.Count(); i++)
            {
                Console.WriteLine($"{i}: {names[i]}");
            }
        }
    }
}