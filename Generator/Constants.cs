using System.Collections.Frozen;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using RPS.SADX.PopTracker.Generator.Models.PopTracker;

namespace RPS.SADX.PopTracker.Generator;

internal static class Constants
{
    internal const string PackRoot = "../../../../pack";

    internal static readonly IReadOnlyDictionary<int, string> MissionBriefs = new Dictionary<int, string>
    {
        { 1, "Bring the man who is standing in front of the burger shop!" },
        { 2, "Get the balloon in the skies of Mystic Ruins!" },
        { 3, "Collect 100 rings, and go to Sonic's billboard by the pool!" },
        { 4, "Weeds are growing all over my place! I must get rid of them!" },
        { 5, "I lost my balloon! It's way up there now!" },
        { 6, "He is going to drown! Help the man in the water!" },
        { 7, "Lonely Metal Sonic needs a friend. Look carefully." },
        { 8, "The Medallion fell under there! No illegal parking please!" },
        { 9, "Get the balloon floating behind the waterfall of the emerald sea." },
        { 10, "What is that sparkling in the water?" },
        { 11, "Destroy the windmill and proceed. Find the balloon in orbit!" },
        { 12, "Who is Chao a good friend with? And what is hidden underneath it?" },
        { 13, "I can't take a shower like this! Do something!" },
        { 14, "I am the keeper of this hotel! Catch me if you can!" },
        { 15, "My medallions got swept away by the tornado! Somebody help me get them back!" },
        { 16, "Get the flags from the floating islands!" },
        { 17, "Aim and shoot all the medallions with a Sonic Ball." },
        { 18, "During night, at the amusement park, place your jumps on the top of one of the tables." },
        { 19, "What is that behind the mirror?" },
        { 20, "Get all the medallions within the time limit! It's really slippery, so be careful!" },
        { 21, "Protect the Sonic Doll from the Spinners around it!" },
        { 22, "Find the flag hidden in the secret passage under the emerald ocean!" },
        { 23, "Go around the wooden horse and collect 10 balloons!" },
        { 24, "'I hate this dark and filthy place!' Can you find it?" },
        { 25, "What is hidden under the lion's right hand?" },
        { 26, "What is that on top of the ship's mast that the pirates are protecting?" },
        { 27, "Collect 100 rings and head to the heliport!" },
        { 28, "During the morning traffic, use the fountain to get to the balloon." },
        { 29, "I am the keeper of this canal! Catch me if you can!" },
        { 30, "A fugitive has escaped from the jail of burning hell! Find the fugitive!" },
        { 31, "Get the balloon as you float in the air along with the trash!" },
        { 32, "Can you get the balloon that is hidden under the bridge?" },
        { 33, "Shoot yourself out of the cannon and get the balloon!" },
        { 34, "Can you get the balloon that is hidden on the ship's bridge?" },
        { 35, "I am the keeper of this icy lake! Catch me if you can!" },
        { 36, "Fighter aircraft are flying everywhere. Somebody get me out of here!" },
        { 37, "Fly over the jungle and get all the balloons!" },
        { 38, "A message from an ancient people: In the direction where the burning arrow is pointing you will see..." },
        { 39, "Treasure hunt at the beach! Find all the medallions under the time limit!" },
        { 40, "What is hidden in the area that the giant snake is staring at?" },
        { 41, "Look real carefully just as you fall from the waterfall!" },
        { 42, "I can't get into the bathroom. How could I've let something like this happened to me?" },
        { 43, "Fortress of steel. High jump on three narrow paths. Be careful not to fall." },
        { 44, "I am the keeper of this ship! Catch me if you can!" },
        { 45, "Go to a place where the rings are laid in the shape of Sonic's face!" },
        { 46, "A secret base that's full of mechanical traps. Pay attention, and you might see..." },
        { 47, "Get ten balloons on the field under the time limit!" },
        { 48, "Can you get the medallion that the giant Sonic is staring at?" },
        { 49, "Scorch through the track, and get all the flags!" },
        { 50, "Select a road that splits into 5 paths before time runs out!" },
        { 51, "Gunman of the Windy Valley. Destroy all Spinners under a time limit!" },
        { 52, "Get 3 flags in the jungle under a time limit!" },
        { 53, "Pass the line of rings with 3 Super High Jumps on the ski slopes!" },
        { 54, "Slide downhill in a blizzard and get all the flags!" },
        { 55, "Run down the building to get all the balloons!" },
        { 56, "Relentless eruptions occur in the flaming canyon. What could be hidden in the area she's staring at?" },
        { 57, "Peak of the volcanic mountain! Watch out for the lava!" },
        { 58, "The big rock will start rolling after you! Try and get all the flags!" },
        { 59, "Watch out for the barrels, and find the hidden flag inside the container!" },
        { 60, "Something is hidden inside the dinosaurs mouth. Can you find it?" },
    }.ToFrozenDictionary();

    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        Converters = { new ItemConverter() },
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true,
        IndentSize = 4
    };

    private sealed class ItemConverter : JsonConverter<Item>
    {
        public override Item? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => throw new NotImplementedException();

        public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case ToggleItem toggle:
                    JsonSerializer.Serialize(writer, toggle, options);
                    break;

                case CollectibleItem collectible:
                    JsonSerializer.Serialize(writer, collectible, options);
                    break;

                case ProgressiveItem progressive:
                    JsonSerializer.Serialize(writer, progressive, options);
                    break;

                default:
                    JsonSerializer.Serialize(writer, value, options);
                    break;
            }
        }
    }
}