using UnityEngine;
using UnityEditor;

public class DialogueGenerator
{
    [MenuItem("Tools/Generate ScrappSalvage Dialogue")]
    public static void CreateScrappSalvageDialogue()
    {
        // Create an instance of the Dialogue ScriptableObject
        Dialogue dialogue = ScriptableObject.CreateInstance<Dialogue>();

        // Set up the dialogue lines
        dialogue.lines = new Dialogue.DialogueLine[16];

        // Line 1: Scrapp
        dialogue.lines[0] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null, // Assign Scrapp's portrait in the Inspector later
            sentence = "You know, Salva, if you’d just see it my way… this trash mountain would look like a diamond mountain.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 2: Salvage
        dialogue.lines[1] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null, // Assign Salvage's portrait in the Inspector later
            sentence = "Only diamond I see here is the one buried under five tons of broken toilets and plastic bags.",
            side = Dialogue.CharacterSide.Right
        };

        // Line 3: Scrapp
        dialogue.lines[2] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "And what’s wrong with that? One ‘bot’s trash is another bot’s treasure!",
            side = Dialogue.CharacterSide.Left
        };

        // Line 4: Salvage
        dialogue.lines[3] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "Can’t believe we’re out here searching for another Steve-E. What was wrong with the last one, and the one before last one, again?",
            side = Dialogue.CharacterSide.Right
        };

        // Line 5: Scrapp
        dialogue.lines[4] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "Steve-E-193 had… well, let’s say an ‘accident’ with the shredder drone. I told him to zig, and he zagged, 192 soaked itself in a green pond.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 6: Salvage
        dialogue.lines[5] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "These machines, what a crap… All right, let’s find 194 before he decides to join his siblings under this ‘diamond mine’ of yours.",
            side = Dialogue.CharacterSide.Right
        };

        // Line 7: Salvage
        dialogue.lines[6] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "Hey, Scrap! Got something here. Think it’s him?",
            side = Dialogue.CharacterSide.Right
        };

        // Line 8: Scrapp
        dialogue.lines[7] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "(wipes some dust off) Let me see… yep, that’s our Steve-E-194! Still in one piece, too. Let’s haul him back to base and see what he remembers.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 9: Scrapp
        dialogue.lines[8] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "All right, let’s hear what you’ve got to say, Steve-E.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 10: Salvage
        dialogue.lines[9] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "Not exactly a chatterbox, is he?",
            side = Dialogue.CharacterSide.Right
        };

        // Line 11: Scrapp
        dialogue.lines[10] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "(fiddling with the bot’s settings) Strange… he should be talking. But wait, this—this memory core… it’s like he’s stuck back when Earth was still green. Look, rivers, fields… a whole different world. Poor thing has no idea what he’s woken up to.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 12: Salvage
        dialogue.lines[11] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "He’ll have to learn fast. This place isn’t a picnic anymore.",
            side = Dialogue.CharacterSide.Right
        };

        // Line 13: Scrapp
        dialogue.lines[12] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "(to the bot) Don’t worry, buddy. We’ll catch you up. Just need to get you used to a world of scrap and salvage, that’s all.",
            side = Dialogue.CharacterSide.Left
        };

        // Line 14: Scrapp
        dialogue.lines[13] = new Dialogue.DialogueLine
        {
            characterName = "Scrapp",
            characterPortrait = null,
            sentence = "PAT PAT",
            side = Dialogue.CharacterSide.Left
        };

        // Line 15: Salvage
        dialogue.lines[14] = new Dialogue.DialogueLine
        {
            characterName = "Salvage",
            characterPortrait = null,
            sentence = "Yeah. Adapting is survival now. Guess that’s the next lesson for him.",
            side = Dialogue.CharacterSide.Right
        };

        // Save the ScriptableObject as an asset
        AssetDatabase.CreateAsset(dialogue, "Assets/ScrappSalvageDialogue.asset");
        AssetDatabase.SaveAssets();

        // Select the newly created asset in the Project window
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = dialogue;

        Debug.Log("ScrappSalvageDialogue ScriptableObject created successfully!");
    }
}
