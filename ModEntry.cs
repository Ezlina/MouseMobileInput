
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using Microsoft.Xna.Framework.Input;

namespace MouseMobile
{
    public class ModEntry : Mod
    {
        private bool mouseMode = true;
        private int lastScroll = 0;

        public override void Entry(IModHelper helper)
        {
            helper.Events.Input.ButtonPressed += OnButtonPressed;
            helper.Events.GameLoop.UpdateTicked += OnUpdate;
        }

        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button == SButton.F8)
            {
                mouseMode = !mouseMode;
                Game1.addHUDMessage(new HUDMessage("Mouse Mode: " + (mouseMode ? "ON" : "OFF")));
            }
        }

        private void OnUpdate(object sender, UpdateTickedEventArgs e)
        {
            if (!Context.IsWorldReady || !mouseMode)
                return;

            MouseState mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                Game1.pressUseToolButton();
            }

            if (mouse.RightButton == ButtonState.Pressed)
            {
                Game1.pressActionButton(Game1.player);
            }

            if (mouse.ScrollWheelValue != lastScroll)
            {
                if (mouse.ScrollWheelValue > lastScroll)
                    Game1.player.CurrentToolIndex++;
                else
                    Game1.player.CurrentToolIndex--;

                lastScroll = mouse.ScrollWheelValue;
            }
        }
    }
