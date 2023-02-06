using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(StatusManager))]
public class DebugController : MonoBehaviour
{
    private bool openDebug;
    private bool showHelp;
    string input;
    public List<object> commandList;
    private Vector2 scroll;
    [SerializeField] private StatusManager statusManager;
    [SerializeField] private PlayerStats stats;
    EssenceManager essenceManager;
    private void Awake()
    {
        statusManager = GetComponent<StatusManager>();
        stats = FindFirstObjectByType<PlayerStats>();
        essenceManager = GetComponent<EssenceManager>();
        AddCommandList();
    }

    private void OnToggleDebug()
    {
        openDebug = !openDebug;
        showHelp = false;
        input = null;
    }
    private void OnGUI()
    {
        float y = 0f;
        if (!openDebug)
        {
            return;
        }

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, y + 5, Screen.width - 30, 20 * commandList.Count);
            scroll = GUI.BeginScrollView(new Rect(0, y + 5, Screen.width, 90), scroll, viewport);
            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command.CommandFormat} - {command.commandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();
            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

    }
    public void OnReturn()
    {


        if (!string.IsNullOrEmpty(input))
        {
            HandleInput();
            input = null;
        }
    }
    private void HandleInput()
    {
        string[] properties = input.Split(' ');
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            if (properties[0] == commandBase.commandId)
            {
                Debug.Log(properties[0]);
                if (commandList[i] == null) { Debug.Log("Not Valid"); return; }

                if (commandList[i] is DebugCommand) ((DebugCommand)commandList[i]).Invoke();
                if (commandList[i] is DebugCommand<int>) (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                if (commandList[i] is DebugCommand<string>) (commandList[i] as DebugCommand<string>).Invoke(properties[1]);

            }
        }
    }


    private void AddCommandList()
    {
        commandList = new()
        {
            CreateHelp(),
            CreateHeat(),
            CreateSetSpawner(),
            CreateRapidFire(),
            CreateKillAll(),
            CreateNeverDie(),
            CreateSetEssence(),
            CreateSetBuff(),
            CreateSetHealth(),
            CreatePeacefull(),
            CreateWTW()
        };

    }
    private DebugCommand CreateHelp()
    => new("help", "Shows list of command", "help", () => { showHelp = true; });
    private DebugCommand CreateHeat()
    => new("heat", "Seeking with Rapid Fire. LOL", "heat", () => { statusManager.CurrentStatus = Status.Seeking; PlayerAttack.RapidFire(); });
    private DebugCommand<string> CreateSetSpawner()
    => new("setspawner", "set spawner on or off", "setspawner <on/off>", (x) => SetSpawnerState(x));
    private DebugCommand CreateRapidFire()
    => new("rapidfire", "normal attacks don't have a cooldown", "rapidfire", () => PlayerAttack.RapidFire());
    private DebugCommand CreateKillAll()
    => new("killall", "Removes all enemies from the scene", "killall", () => KillAllEnemies());



    private DebugCommand CreateNeverDie()
    => new("neverdie", "Health won't be reduced while active", "neverdie", () => stats.ToggleNeverDie());
    private DebugCommand<int> CreateSetEssence()
    => new("setessence", "Sets the essence amount", "set essence <essence amount>", (x) => essenceManager.Essence = x);
    private DebugCommand<string> CreateSetBuff()
    => new("setbuff", "Sets current buff. Note: the buff will not end until you change it to None or the essence bar fills up", "setbuff <Name>", (x) => SetStatus(x));
    private DebugCommand<int> CreateSetHealth()
    => new("sethealth", "Sets the current health of the player", "sethealth <amount>", (x) => stats.Health = x);
    private DebugCommand CreatePeacefull()
    => new("peaceful", "removes all enemies and turns spawners off", "peacefull", () => Peacefull());

    private void Peacefull()
    {
        SetSpawnerState("off");
        KillAllEnemies();
    }


    private DebugCommand CreateWTW()
            => new("wtw", "walk through walls", "wtw", () => WalkThroughWalls());

    private void KillAllEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy);
        }

    }
    private void SetStatus(string x)
    {
        foreach (Status status in Enum.GetValues(typeof(Status)))
        {
            if (status.ToString().ToLower().Equals(x.ToLower())) statusManager.CurrentStatus = status;
        }
    }
    private void SetSpawnerState(string state)
    {
        bool OnOrOff = state == "on";
        Spawner[] spawnerList;
        spawnerList = FindObjectsOfType<Spawner>();
        foreach (Spawner spawner in spawnerList)
        {
            spawner.StopAllCoroutines();
            spawner.IsSpawnable = OnOrOff;
        }
    }
    private void WalkThroughWalls()
    {
        Collider2D col = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        col.enabled = !col.enabled;
    }
}
