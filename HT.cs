using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

public class HT : PhysicsGame
{
    /// Pelaajan liike

    Vector nopeusYlos = new Vector(0, 2000);
    Vector nopeusAlas = new Vector(0, -2000);
    Vector nopeusVasemmalle = new Vector(-2000, 0);
    Vector nopeusOikealle = new Vector(2000, 0);


    public override void Begin()
    {
        Level.BackgroundColor = Color.Beige;
        Level.CreateBorders();

        PhysicsObject pelaaja = LuoNelikulmio(this, "pelaaja1");
        PhysicsObject vesa = LuoNelikulmio(this, "vesa");
        PhysicsObject kyna = Kynat(this);

        AddCollisionHandler(pelaaja, kyna, KasittelePallonTormays);

        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä");
        Keyboard.Listen(Key.Up, ButtonState.Down, LyoUkkoa, "lyö", pelaaja, nopeusYlos);
        Keyboard.Listen(Key.Down, ButtonState.Down, LyoUkkoa, "lyö a", pelaaja, nopeusAlas);
        Keyboard.Listen(Key.Right, ButtonState.Down, LyoUkkoa, "lyö oi", pelaaja, nopeusOikealle);
        Keyboard.Listen(Key.Left, ButtonState.Down, LyoUkkoa, "lyö v", pelaaja, nopeusVasemmalle);
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, Exit, "Lopeta peli");

        


    }

    public static PhysicsObject LuoNelikulmio(PhysicsGame peli, string tunniste)
    {
        PhysicsObject ukko = new PhysicsObject(70, 100, Shape.Rectangle);
        ukko.Color = Color.Black;
        
        // Vector suunta = new Vector(vauhti, vauhti);
        // ukko.Hit(suunta);
        ukko.Tag = tunniste;
        // ukko.Mass = 1.0;
        peli.Add(ukko);
        ukko.LinearDamping = 0.93;
        ukko.Restitution = 0;
        ukko.AngularDamping = 0.9;
        ukko.MaxVelocity = 40000;
        
        return ukko;
    }

    public static void LyoUkkoa(PhysicsObject ukko, Vector suunta)
    {
        ukko.Push(suunta);
    }

    public static PhysicsObject Kynat(PhysicsGame peli)
    {
        PhysicsObject kyna = new PhysicsObject(20, 40);
        kyna.Color = Color.Red;
        peli.Add(kyna);
        kyna.Y = 100;
        kyna.X = 100;
        Vector suunta = RandomGen.NextVector(100, 200);
        kyna.Hit(suunta);

    

        return kyna;
    }



    void KasittelePallonTormays(PhysicsObject pelaaja, PhysicsObject kyna)
    {
        Explosion rajahdys = new Explosion(kyna.Width * 2);
        rajahdys.Position = kyna.Position;
        rajahdys.UseShockWave = false;
        this.Add(rajahdys);
        Remove(kyna);
    }

    // Moro Valte
}