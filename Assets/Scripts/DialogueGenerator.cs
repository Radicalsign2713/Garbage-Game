using UnityEngine;
using UnityEditor;

public class DialogueGenerator
{
    [MenuItem("Tools/Generate SalvageScrapp Dialogue")]
    public static void CreateSalvageScrappDialogue()
    {
        // Create an instance of the Dialogue ScriptableObject
        Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();

        // Set up the dialogue lines
        dialogue.lines = new Dialogue.DialogueLine[10];

        // Line 1: Salvage
        dialogue.lines[0] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null, // Assign Salvage's portrait in the Inspector later
            sentence = "All right, Steve-E, time to earn your keep. We’re part of the HUMAN FLY FAR project—last-ditch plan to get off this wreck of a planet. While most of the big rockets are already gone, a few of us stayed behind, prepping for one final shot at survival. We are building Small rockets, each just strong enough to make it to Mars… if we’re lucky.",
            side = Dialogue.CharacterSide.Right
        };

        // Line 2: Scrapp
        dialogue.lines[1] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null, // Assign Scrapp's portrait in the Inspector later
            sentence = "Yeah, picture it—a bunch of scrappy, rusty little rockets blasting out of Earth like, whoosh! Just us and a few leftover nuts and bolts, headed to a whole new life on the red planet. That’s the plan! And you, Steve-E, get to help make it happen by fetching us the goodies we need.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 3: Salvage
        dialogue.lines[2] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "‘Goodies’ being whatever scrap, metal, fuel cells, and circuits you can dig up in the wild. Everything here is either broken, buried, or both, so you’ll be sent out to salvage what’s left. Every bolt you bring back means one more piece we can use to build these rockets.",
            side = Dialogue.CharacterSide.Right
        };

        // Line 4: Scrapp
        dialogue.lines[3] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "Think of it as treasure hunting! But, y’know… with a lot more rust, radiation, and roaches.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 5: Salvage
        dialogue.lines[4] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "This isn’t playtime, Scrap. The project’s all we’ve got. And if we fail… well, we won’t get another chance.",
            side = Dialogue.CharacterSide.Right
        };

        // Line 6: Scrapp
        dialogue.lines[5] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "Right, right, serious stuff. So, Steve-E, just bring us whatever junk you find, and we’ll make it shine. Trust us—we’ve turned worse into wonders.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 7: Salvage
        dialogue.lines[6] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "We’re counting on you, Steve-E. Now, get out there and start scavenging. Open the Gate.",
            side = Dialogue.CharacterSide.Right
        };

        // Save the ScriptableObject as an asset
        AssetDatabase.CreateAsset(dialogue, "Assets/SalvageScrappDialogue.asset");
        AssetDatabase.SaveAssets();

        // Select the newly created asset in the Project window
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = dialogue;

        Debug.Log("SalvageScrappDialogue ScriptableObject created successfully!");
    }
}
