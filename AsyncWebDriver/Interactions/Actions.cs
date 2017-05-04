// Copyright (c) Oleg Zudov. All Rights Reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Zu.AsyncWebDriver.Internal;

namespace Zu.AsyncWebDriver.Interactions
{
    /// <summary>
    ///     Provides a mechanism for building advanced interactions with the browser.
    /// </summary>
    public class Actions
    {
        private readonly IKeyboard keyboard;
        private readonly IMouse mouse;
        private CompositeAction action = new CompositeAction();

        /// <summary>
        ///     Initializes a new instance of the <see cref="Actions" /> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver" /> object on which the actions built will be performed.</param>
        public Actions(IWebDriver driver)
        {
            var inputDevicesDriver = driver as IHasInputDevices;
            if (inputDevicesDriver == null)
            {
                var wrapper = driver as IWrapsDriver;
                while (wrapper != null)
                {
                    inputDevicesDriver = wrapper.WrappedDriver as IHasInputDevices;
                    if (inputDevicesDriver != null)
                        break;
                    wrapper = wrapper.WrappedDriver as IWrapsDriver;
                }
            }

            if (inputDevicesDriver == null)
                throw new ArgumentException(
                    "The IWebDriver object must implement or wrap a driver that implements IHasInputDevices.",
                    "driver");
            keyboard = inputDevicesDriver.Keyboard;
            mouse = inputDevicesDriver.Mouse;
        }

        /// <summary>
        ///     Sends a modifier key down message to the browser.
        /// </summary>
        /// <param name="theKey">The key to be sent.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        /// <exception cref="ArgumentException">
        ///     If the key sent is not is not one
        ///     of <see cref="Keys.Shift" />, <see cref="Keys.Control" />, or <see cref="Keys.Alt" />.
        /// </exception>
        public Actions KeyDown(string theKey)
        {
            return KeyDown(null, theKey);
        }

        /// <summary>
        ///     Sends a modifier key down message to the specified element in the browser.
        /// </summary>
        /// <param name="element">The element to which to send the key command.</param>
        /// <param name="theKey">The key to be sent.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        /// <exception cref="ArgumentException">
        ///     If the key sent is not is not one
        ///     of <see cref="Keys.Shift" />, <see cref="Keys.Control" />, or <see cref="Keys.Alt" />.
        /// </exception>
        public Actions KeyDown(IWebElement element, string theKey)
        {
            var target = GetLocatableFromElement(element);
            action.AddAction(new KeyDownAction(keyboard, mouse, target, theKey));
            return this;
        }

        /// <summary>
        ///     Sends a modifier key up message to the browser.
        /// </summary>
        /// <param name="theKey">The key to be sent.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        /// <exception cref="ArgumentException">
        ///     If the key sent is not is not one
        ///     of <see cref="Keys.Shift" />, <see cref="Keys.Control" />, or <see cref="Keys.Alt" />.
        /// </exception>
        public Actions KeyUp(string theKey)
        {
            return KeyUp(null, theKey);
        }

        /// <summary>
        ///     Sends a modifier up down message to the specified element in the browser.
        /// </summary>
        /// <param name="element">The element to which to send the key command.</param>
        /// <param name="theKey">The key to be sent.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        /// <exception cref="ArgumentException">
        ///     If the key sent is not is not one
        ///     of <see cref="Keys.Shift" />, <see cref="Keys.Control" />, or <see cref="Keys.Alt" />.
        /// </exception>
        public Actions KeyUp(IWebElement element, string theKey)
        {
            var target = GetLocatableFromElement(element);
            action.AddAction(new KeyUpAction(keyboard, mouse, target, theKey));
            return this;
        }

        /// <summary>
        ///     Sends a sequence of keystrokes to the browser.
        /// </summary>
        /// <param name="keysToSend">The keystrokes to send to the browser.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions SendKeys(string keysToSend)
        {
            return SendKeys(null, keysToSend);
        }

        /// <summary>
        ///     Sends a sequence of keystrokes to the specified element in the browser.
        /// </summary>
        /// <param name="element">The element to which to send the keystrokes.</param>
        /// <param name="keysToSend">The keystrokes to send to the browser.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions SendKeys(IWebElement element, string keysToSend)
        {
            var target = GetLocatableFromElement(element);
            action.AddAction(new SendKeysAction(keyboard, mouse, target, keysToSend));
            return this;
        }

        /// <summary>
        ///     Clicks and holds the mouse button down on the specified element.
        /// </summary>
        /// <param name="onElement">The element on which to click and hold.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions ClickAndHold(IWebElement onElement)
        {
            var target = GetLocatableFromElement(onElement);
            action.AddAction(new ClickAndHoldAction(mouse, target));
            return this;
        }

        /// <summary>
        ///     Clicks and holds the mouse button at the last known mouse coordinates.
        /// </summary>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions ClickAndHold()
        {
            return ClickAndHold(null);
        }

        /// <summary>
        ///     Releases the mouse button on the specified element.
        /// </summary>
        /// <param name="onElement">The element on which to release the button.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions Release(IWebElement onElement)
        {
            var target = GetLocatableFromElement(onElement);
            action.AddAction(new ButtonReleaseAction(mouse, target));
            return this;
        }

        /// <summary>
        ///     Releases the mouse button at the last known mouse coordinates.
        /// </summary>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions Release()
        {
            return Release(null);
        }

        /// <summary>
        ///     Clicks the mouse on the specified element.
        /// </summary>
        /// <param name="onElement">The element on which to click.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions Click(IWebElement onElement)
        {
            var target = GetLocatableFromElement(onElement);
            action.AddAction(new ClickAction(mouse, target));
            return this;
        }

        /// <summary>
        ///     Clicks the mouse at the last known mouse coordinates.
        /// </summary>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions Click()
        {
            return Click(null);
        }

        /// <summary>
        ///     Double-clicks the mouse on the specified element.
        /// </summary>
        /// <param name="onElement">The element on which to double-click.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions DoubleClick(IWebElement onElement)
        {
            var target = GetLocatableFromElement(onElement);
            action.AddAction(new DoubleClickAction(mouse, target));
            return this;
        }

        /// <summary>
        ///     Double-clicks the mouse at the last known mouse coordinates.
        /// </summary>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions DoubleClick()
        {
            return DoubleClick(null);
        }

        /// <summary>
        ///     Moves the mouse to the specified element.
        /// </summary>
        /// <param name="toElement">The element to which to move the mouse.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions MoveToElement(IWebElement toElement)
        {
            var target = GetLocatableFromElement(toElement);
            action.AddAction(new MoveMouseAction(mouse, target));
            return this;
        }

        /// <summary>
        ///     Moves the mouse to the specified offset of the top-left corner of the specified element.
        /// </summary>
        /// <param name="toElement">The element to which to move the mouse.</param>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions MoveToElement(IWebElement toElement, int offsetX, int offsetY)
        {
            var target = GetLocatableFromElement(toElement);
            action.AddAction(new MoveToOffsetAction(mouse, target, offsetX, offsetY));
            return this;
        }

        /// <summary>
        ///     Moves the mouse to the specified offset of the last known mouse coordinates.
        /// </summary>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions MoveByOffset(int offsetX, int offsetY)
        {
            return MoveToElement(null, offsetX, offsetY);
        }

        /// <summary>
        ///     Right-clicks the mouse on the specified element.
        /// </summary>
        /// <param name="onElement">The element on which to right-click.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions ContextClick(IWebElement onElement)
        {
            var target = GetLocatableFromElement(onElement);
            action.AddAction(new ContextClickAction(mouse, target));
            return this;
        }

        /// <summary>
        ///     Right-clicks the mouse at the last known mouse coordinates.
        /// </summary>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions ContextClick()
        {
            return ContextClick(null);
        }

        /// <summary>
        ///     Performs a drag-and-drop operation from one element to another.
        /// </summary>
        /// <param name="source">The element on which the drag operation is started.</param>
        /// <param name="target">The element on which the drop is performed.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions DragAndDrop(IWebElement source, IWebElement target)
        {
            var startElement = GetLocatableFromElement(source);
            var endElement = GetLocatableFromElement(target);
            action.AddAction(new ClickAndHoldAction(mouse, startElement));
            action.AddAction(new MoveMouseAction(mouse, endElement));
            action.AddAction(new ButtonReleaseAction(mouse, endElement));
            return this;
        }

        /// <summary>
        ///     Performs a drag-and-drop operation on one element to a specified offset.
        /// </summary>
        /// <param name="source">The element on which the drag operation is started.</param>
        /// <param name="offsetX">The horizontal offset to which to move the mouse.</param>
        /// <param name="offsetY">The vertical offset to which to move the mouse.</param>
        /// <returns>A self-reference to this <see cref="Actions" />.</returns>
        public Actions DragAndDropToOffset(IWebElement source, int offsetX, int offsetY)
        {
            var startElement = GetLocatableFromElement(source);
            action.AddAction(new ClickAndHoldAction(mouse, startElement));
            action.AddAction(new MoveToOffsetAction(mouse, null, offsetX, offsetY));
            action.AddAction(new ButtonReleaseAction(mouse, null));
            return this;
        }

        /// <summary>
        ///     Builds the sequence of actions.
        /// </summary>
        /// <returns>A composite <see cref="IAction" /> which can be used to perform the actions.</returns>
        public IAction Build()
        {
            var toReturn = action;
            action = new CompositeAction();
            return toReturn;
        }

        /// <summary>
        ///     Performs the currently built action.
        /// </summary>
        public void Perform()
        {
            Build().Perform();
        }

        /// <summary>
        ///     Gets the <see cref="ILocatable" /> instance of the specified <see cref="IWebElement" />.
        /// </summary>
        /// <param name="element">The <see cref="IWebElement" /> to get the location of.</param>
        /// <returns>The <see cref="ILocatable" /> of the <see cref="IWebElement" />.</returns>
        protected static ILocatable GetLocatableFromElement(IWebElement element)
        {
            if (element == null)
                return null;
            var target = element as ILocatable;
            if (target == null)
            {
                var wrapper = element as IWrapsElement;
                while (wrapper != null)
                {
                    target = wrapper.WrappedElement as ILocatable;
                    if (target != null)
                        break;
                    wrapper = wrapper.WrappedElement as IWrapsElement;
                }
            }

            if (target == null)
                throw new ArgumentException(
                    "The IWebElement object must implement or wrap an element that implements ILocatable.", "element");
            return target;
        }

        /// <summary>
        ///     Adds an action to current list of actions to be performed.
        /// </summary>
        /// <param name="actionToAdd">The <see cref="IAction" /> to be added.</param>
        protected void AddAction(IAction actionToAdd)
        {
            action.AddAction(actionToAdd);
        }
    }
}