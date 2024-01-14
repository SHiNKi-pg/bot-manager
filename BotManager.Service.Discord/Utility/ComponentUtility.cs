using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotManager.Service.Discord.Utility
{
    /// <summary>
    /// メッセージコンポーネントユーティリティ
    /// </summary>
    public static class ComponentUtility
    {
        /// <summary>
        /// コンポーネントを作成します。
        /// </summary>
        /// <param name="builder">コンポーネントビルダ</param>
        /// <returns></returns>
        public static MessageComponent CreateComponent(Action<ComponentBuilder> builder)
        {
            ComponentBuilder componentBuilder = new();
            builder(componentBuilder);
            return componentBuilder.Build();
        }

        /// <summary>
        /// コンポーネントに行を追加します。
        /// </summary>
        /// <param name="componentBuilder"></param>
        /// <param name="builder">コンポーネント行ビルダ</param>
        /// <returns></returns>
        public static ComponentBuilder AddRow(this ComponentBuilder componentBuilder, Action<ActionRowBuilder> builder)
        {
            ActionRowBuilder actionRowBuilder = new();
            builder(actionRowBuilder);
            return componentBuilder.AddRow(actionRowBuilder);
        }

        /// <summary>
        /// モーダルを作成します。
        /// </summary>
        /// <param name="builder">モーダルビルダ</param>
        /// <returns></returns>
        public static Modal CreateModal(Action<ModalBuilder> builder)
        {
            ModalBuilder modalBuilder = new();
            builder(modalBuilder);
            return modalBuilder.Build();
        }

        /// <summary>
        /// セレクトメニューコンポーネントを作成します。
        /// </summary>
        /// <param name="builder">コンポーネントビルダ</param>
        /// <returns></returns>
        public static SelectMenuComponent CreateSelectMenu(Action<SelectMenuBuilder> builder)
        {
            SelectMenuBuilder selectMenuBuilder = new();
            builder(selectMenuBuilder);
            return selectMenuBuilder.Build();
        }
    }
}
