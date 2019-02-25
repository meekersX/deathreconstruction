using System.IO;

public class CM_Communication {

    //  public override bool acceptMessageData(BinaryReader messageDataReader, TreeView outputTreeView) {
    //      bool handled = true;

    //      PacketOpcode opcode = Util.readOpcode(messageDataReader);
    //      switch (opcode) {
    //          case PacketOpcode.Evt_Communication__Talk_ID: // 0x0015
    //              {
    //                  var message = Talk.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__PopUpString_ID: // 0x0004
    //              {
    //                  var message = PopUpString.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__SetAFKMode_ID: // 0x000F
    //              {
    //                  var message = SetAFKMode.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__SetAFKMessage_ID: // 0x0010
    //              {
    //                  var message = SetAFKMessage.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__TalkDirect_ID: // 0x0032
    //              {
    //                  var message = TalkDirect.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__ModifyCharacterSquelch_ID: // 0x0058
    //              {
    //                  var message = ModifyCharacterSquelch.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__ModifyAccountSquelch_ID: // 0x0059
    //              {
    //                  var message = ModifyAccountSquelch.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__ModifyGlobalSquelch_ID: // 0x005B
    //              {
    //                  var message = ModifyGlobalSquelch.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__TalkDirectByName_ID: // 0x005D
    //              {
    //                  var message = TalkDirectByName.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__ChannelBroadcast_ID: // 0x0147
    //              {
    //                  var message = ChannelBroadcast.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__SetSquelchDB_ID: // 0x01F4
    //              {
    //                  var message = SetSquelchDB.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__Emote_ID: // 0x01DF
    //              {
    //                  var message = Emote.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__HearEmote_ID: // 0x01E0
    //              {
    //                  var message = HearEmote.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__SoulEmote_ID: // 0x01E1
    //              {
    //                  var message = SoulEmote.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__HearSoulEmote_ID: // 0x01E2
    //              {
    //                  var message = HearSoulEmote.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__Recv_ChatRoomTracker_ID: // 0x0295
    //              {
    //                  var message = Recv_ChatRoomTracker.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__WeenieError_ID: // 0x028A
    //              {
    //                  WeenieError message = WeenieError.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__WeenieErrorWithString_ID: // 0x028B
    //              {
    //                  WeenieErrorWithString message = WeenieErrorWithString.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__HearSpeech_ID: // 0x02BB
    //              {
    //                  var message = HearSpeech.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__HearRangedSpeech_ID: // 0x02BC
    //              {
    //                  var message = HearRangedSpeech.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__HearDirectSpeech_ID: // 0x2BD
    //              {
    //                  var message = HearDirectSpeech.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__TransientString_ID: // 0x2EB
    //              {
    //                  var message = TransientString.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          case PacketOpcode.Evt_Communication__TextboxString_ID: // 0xF7E0
    //              {
    //                  var message = TextBoxString.read(messageDataReader);
    //                  message.contributeToTreeView(outputTreeView);
    //                  break;
    //              }
    //          default: {
    //                  handled = false;
    //                  break;
    //              }
    //      }

    //      return handled;
    //  }

    //  public class Talk : Message
    //  {
    //      public PStringChar MessageText;

    //      public static Talk read(BinaryReader binaryReader)
    //      {
    //          var newObj = new Talk();
    //          newObj.MessageText = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("MessageText = " + MessageText.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = MessageText.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class PopUpString : Message
    //  {
    //      public PStringChar PopUpMessage;
    //      public static PopUpString read(BinaryReader binaryReader)
    //      {
    //          var newObj = new PopUpString();
    //          newObj.PopUpMessage = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //          rootNode.Nodes.Add("PopUpMessage = " + PopUpMessage.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = PopUpMessage.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class SetAFKMode : Message
    //  {
    //      public uint i_bAFK;
    //      public static SetAFKMode read(BinaryReader binaryReader)
    //      {
    //          var newObj = new SetAFKMode();
    //          newObj.i_bAFK = binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("i_bAFK = " + i_bAFK);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class SetAFKMessage : Message
    //  {
    //      public PStringChar i_strMessage;
    //      public static SetAFKMessage read(BinaryReader binaryReader)
    //      {
    //          var newObj = new SetAFKMessage();
    //          newObj.i_strMessage = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("i_strMessage = " + i_strMessage.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = i_strMessage.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class TalkDirect : Message
    //  {
    //      public PStringChar MessageText;
    //      public uint TargetID;

    //      public static TalkDirect read(BinaryReader binaryReader)
    //      {
    //          var newObj = new TalkDirect();
    //          newObj.MessageText = PStringChar.read(binaryReader);
    //          newObj.TargetID = binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("MessageText = " + MessageText.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = MessageText.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("TargetID = " + Utility.FormatHex(TargetID));
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class ModifyCharacterSquelch : Message
    //  {
    //      public uint i_add;
    //      public uint i_character_id;
    //      public PStringChar i_character_name;
    //      public uint i_msg_type;

    //      public static ModifyCharacterSquelch read(BinaryReader binaryReader)
    //      {
    //          var newObj = new ModifyCharacterSquelch();
    //          newObj.i_add = binaryReader.ReadUInt32();
    //          newObj.i_character_id = binaryReader.ReadUInt32();
    //          newObj.i_character_name = PStringChar.read(binaryReader);
    //          newObj.i_msg_type = binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("i_add = " + i_add);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("i_character_id = " + Utility.FormatHex(i_character_id));
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //          rootNode.Nodes.Add("i_character_name = " + i_character_name.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = i_character_name.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("i_msg_type = " + (SquelchTypes)i_msg_type);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class ModifyAccountSquelch : Message
    //  {
    //      public uint i_add;
    //      public PStringChar i_character_name;

    //      public static ModifyAccountSquelch read(BinaryReader binaryReader)
    //      {
    //          var newObj = new ModifyAccountSquelch();
    //          newObj.i_add = binaryReader.ReadUInt32();
    //          newObj.i_character_name = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("i_add = " + i_add);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("i_character_name = " + i_character_name.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = i_character_name.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class ModifyGlobalSquelch : Message
    //  {
    //      public uint i_add;
    //      public uint i_msg_type;

    //      public static ModifyGlobalSquelch read(BinaryReader binaryReader)
    //      {
    //          var newObj = new ModifyGlobalSquelch();
    //          newObj.i_add = binaryReader.ReadUInt32();
    //          newObj.i_msg_type = binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("i_add = " + i_add);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("i_msg_type = " + (SquelchTypes)i_msg_type);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class TalkDirectByName : Message
    //  {
    //      public PStringChar MessageText;
    //      public PStringChar TargetName;

    //      public static TalkDirectByName read(BinaryReader binaryReader)
    //      {
    //          var newObj = new TalkDirectByName();
    //          newObj.MessageText = PStringChar.read(binaryReader);
    //          newObj.TargetName = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("MessageText = " + MessageText.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = MessageText.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("TargetName = " + TargetName.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = TargetName.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class ChannelBroadcast : Message
    //  {
    //      public bool SendingBroadcast = false;
    //      public uint Opcode;
    //      public uint GroupChatType;
    //      public PStringChar SenderName;
    //      public ushort SenderNameSize;
    //      public PStringChar MessageText;

    //      public static ChannelBroadcast read(BinaryReader binaryReader)
    //      {
    //          var newObj = new ChannelBroadcast();
    //          // Check for game action (0xF7B1) opcode which indicates a sent broadast. Received messages use a game event (0xF7B0) message instead.
    //          binaryReader.BaseStream.Position = (binaryReader.BaseStream.Position - 12);
    //          newObj.Opcode = binaryReader.ReadUInt32();
    //          if ( newObj.Opcode == 0x0000F7B1) { // Sent broadcasts use 0xF7B1.
    //              newObj.SendingBroadcast = true; 
    //          }
    //          binaryReader.BaseStream.Position = (binaryReader.BaseStream.Position + 8);
    //          newObj.GroupChatType = binaryReader.ReadUInt32();
    //          newObj.SenderNameSize = binaryReader.ReadUInt16();
    //          if (newObj.SenderNameSize == 0) {   // Received a message with no sender name
    //              binaryReader.ReadBytes(2);      // so we skip 2 bytes of padding.
    //              newObj.MessageText = PStringChar.read(binaryReader);
    //          }
    //          else { // Need to seek back 2 bytes and process normally.
    //               binaryReader.BaseStream.Position = (binaryReader.BaseStream.Position - 2);
    //              if (newObj.SendingBroadcast == true) {
    //                  newObj.MessageText = PStringChar.read(binaryReader);
    //              }
    //              else { // Received message with sender and message.
    //                  newObj.SenderName = PStringChar.read(binaryReader);
    //                  newObj.MessageText = PStringChar.read(binaryReader);
    //              }
    //          }
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          if (SendingBroadcast)
    //              ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          else
    //              ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //          rootNode.Nodes.Add("GroupChatType = " + (GroupChatType)GroupChatType);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          if (SenderNameSize == 0 || SendingBroadcast == true) {  // Some received messages and all sent messages do not have a sender name.
    //              if (SenderNameSize == 0)
    //              {
    //                  rootNode.Nodes.Add("SenderName = " + SenderName);
    //                  ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //              }
    //              rootNode.Nodes.Add("MessageText = " + MessageText);
    //              ContextInfo.AddToList(new ContextInfo { Length = MessageText.Length, DataType = DataType.Serialized_AsciiString });
    //          }
    //          else {
    //              rootNode.Nodes.Add("SenderName = " + SenderName);
    //              ContextInfo.AddToList(new ContextInfo { Length = SenderName.Length, DataType = DataType.Serialized_AsciiString });
    //              rootNode.Nodes.Add("MessageText = " + MessageText);
    //              ContextInfo.AddToList(new ContextInfo { Length = MessageText.Length, DataType = DataType.Serialized_AsciiString });
    //          }
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class CharacterHash
    //  {
    //      public uint i_character_id;
    //      public SquelchInfo character_info;
    //      public int Length;

    //      public static CharacterHash read(BinaryReader binaryReader)
    //      {
    //          var newObj = new CharacterHash();
    //          var startPosition = binaryReader.BaseStream.Position;
    //          newObj.i_character_id = binaryReader.ReadUInt32();
    //          newObj.character_info = SquelchInfo.read(binaryReader);
    //          newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
    //          return newObj;
    //      }

    //      public void contributeToTreeNode(TreeNode node)
    //      {
    //          node.Nodes.Add("i_character_id = " + Utility.FormatHex(i_character_id));
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //          character_info.contributeToTreeNode(node);
    //      }
    //  }

    //  public class SquelchInfo
    //  {
    //      public PList<uint> squelch_msgs;
    //      public PStringChar name;
    //      public uint is_zone_squelch;
    //      public int Length;

    //      public static SquelchInfo read(BinaryReader binaryReader)
    //      {
    //          var newObj = new SquelchInfo();
    //          var startPosition = binaryReader.BaseStream.Position;
    //          newObj.squelch_msgs = PList<uint>.read(binaryReader);
    //          newObj.name = PStringChar.read(binaryReader);
    //          newObj.is_zone_squelch = binaryReader.ReadUInt32();
    //          newObj.Length = (int)(binaryReader.BaseStream.Position - startPosition);
    //          return newObj;
    //      }

    //      public void contributeToTreeNode(TreeNode node)
    //      {
    //          if (squelch_msgs.list.Count == 0) {
    //              node.Nodes.Add("squelch_msgs = None");
    //              ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          }
    //          // skip count header
    //          ContextInfo.DataIndex += 4;
    //          for (int i = 0; i < squelch_msgs.list.Count; i++) {
    //              node.Nodes.Add("squelch_msgs = " + (SquelchMasks)squelch_msgs.list[i]);
    //              ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          }
    //          node.Nodes.Add("name = " + name);
    //          ContextInfo.AddToList(new ContextInfo { Length = name.Length, DataType = DataType.Serialized_AsciiString });
    //          node.Nodes.Add("is_zone_squelch = " + is_zone_squelch);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //      }
    //  }

    //  public class SetSquelchDB : Message
    //  {
    //      public ushort account_num_buckets;
    //      public ushort account_table_size;
    //      public ushort character_num_buckets;
    //      public ushort character_table_size;
    //      public List<CharacterHash> character_list = new List<CharacterHash>();
    //      public int character_listLength;
    //      public SquelchInfo global_squelch;

    //      public static SetSquelchDB read(BinaryReader binaryReader)
    //      {
    //          var newObj = new SetSquelchDB();
    //          // Note: Even if the user applies a squelch to an account using the ModifyAccountSquelch message (0x0059) it does not appear 
    //          // in this account squelch info section but rather in the character squelch info section of the server response which comes next.
    //          // It appears based on pcaps that the account squelch info will always be 0 and therefore will not need to be unpacked.
    //          newObj.account_num_buckets = binaryReader.ReadUInt16();
    //          newObj.account_table_size = binaryReader.ReadUInt16();
    //          var startPosition = binaryReader.BaseStream.Position;
    //          newObj.character_num_buckets = binaryReader.ReadUInt16();
    //          newObj.character_table_size = binaryReader.ReadUInt16();
    //          for (int i = 0; i < newObj.character_num_buckets; i++) {
    //              newObj.character_list.Add(CharacterHash.read(binaryReader));
    //          }
    //          newObj.character_listLength = (int)(binaryReader.BaseStream.Position - startPosition);
    //          newObj.global_squelch = SquelchInfo.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //          TreeNode accountNode = rootNode.Nodes.Add("account_hash = ");
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 }, updateDataIndex: false);
    //          accountNode.Nodes.Add("num_buckets = " + account_num_buckets);
    //          ContextInfo.AddToList(new ContextInfo { Length = 2 });
    //          accountNode.Nodes.Add("table_size = " + account_table_size);
    //          ContextInfo.AddToList(new ContextInfo { Length = 2 });
    //          TreeNode characterhashNode = rootNode.Nodes.Add("character_hash = ");
    //          ContextInfo.AddToList(new ContextInfo { Length = character_listLength }, updateDataIndex: false);
    //          characterhashNode.Nodes.Add("num_buckets = " + character_num_buckets);
    //          ContextInfo.AddToList(new ContextInfo { Length = 2 });
    //          characterhashNode.Nodes.Add("table_size = " + character_table_size);
    //          ContextInfo.AddToList(new ContextInfo { Length = 2 });
    //          // add character stuff here
    //          for (int i = 0; i < character_list.Count; i++) {
    //              TreeNode characterNode = characterhashNode.Nodes.Add($"character {i+1} = ");
    //              ContextInfo.AddToList(new ContextInfo { Length = character_list[i].Length }, updateDataIndex: false);
    //              character_list[i].contributeToTreeNode(characterNode);
    //          }
    //          TreeNode globalsquelchNode = rootNode.Nodes.Add("global_squelch = ");
    //          ContextInfo.AddToList(new ContextInfo { Length = global_squelch.Length }, updateDataIndex: false);
    //          global_squelch.contributeToTreeNode(globalsquelchNode);
    //          treeView.Nodes.Add(rootNode);
    //          rootNode.ExpandAll();
    //          rootNode.EnsureVisible();
    //      }
    //  }

    //  public class Emote : Message
    //  {
    //      public PStringChar emoteMessage;

    //      public static Emote read(BinaryReader binaryReader)
    //      {
    //          var newObj = new Emote();
    //          newObj.emoteMessage = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("emoteMessage = " + emoteMessage);
    //          ContextInfo.AddToList(new ContextInfo { Length = emoteMessage.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class HearEmote : Message
    //  {
    //      public uint SenderID;
    //      public PStringChar SenderName;
    //      public PStringChar EmoteMessage;

    //      public static HearEmote read(BinaryReader binaryReader)
    //      {
    //          var newObj = new HearEmote();
    //          newObj.SenderID = binaryReader.ReadUInt32();
    //          newObj.SenderName = PStringChar.read(binaryReader);
    //          newObj.EmoteMessage = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });
    //          rootNode.Nodes.Add("SenderID = " + Utility.FormatHex(this.SenderID));
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //          rootNode.Nodes.Add("SenderName = " + SenderName.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = SenderName.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("EmoteMessage = " + EmoteMessage.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = EmoteMessage.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }
    //  public class SoulEmote : Message
    //  {
    //      public PStringChar EmoteMessage;

    //      public static SoulEmote read(BinaryReader binaryReader)
    //      {
    //          SoulEmote newObj = new SoulEmote();
    //          newObj.EmoteMessage = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ClientToServerHeader });
    //          rootNode.Nodes.Add("EmoteMessage = " + EmoteMessage.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = EmoteMessage.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }
    //  public class HearSoulEmote : Message
    //  {
    //      public uint SenderID;
    //      public PStringChar SenderName;
    //      public PStringChar EmoteMessage;

    //      public static HearSoulEmote read(BinaryReader binaryReader)
    //      {
    //          var newObj = new HearSoulEmote();
    //          newObj.SenderID = binaryReader.ReadUInt32();
    //          newObj.SenderName = PStringChar.read(binaryReader);
    //          newObj.EmoteMessage = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });
    //          rootNode.Nodes.Add("SenderID = " + Utility.FormatHex(this.SenderID));
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //          rootNode.Nodes.Add("SenderName = " + SenderName.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = SenderName.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("EmoteMessage = " + EmoteMessage.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = EmoteMessage.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class Recv_ChatRoomTracker : Message
    //  {
    //      public uint AllegianceChannel;
    //      public uint GeneralChannel;
    //      public uint TradeChannel;
    //      public uint LFGChannel;
    //      public uint RoleplayChannel;
    //      public uint Olthoi;
    //      public uint Society;
    //      public uint SocietyCelHan;
    //      public uint SocietyEldWeb;
    //      public uint SocietyRadBlo;

    //public static Recv_ChatRoomTracker read(BinaryReader binaryReader)
    //      {
    //          var newObj = new Recv_ChatRoomTracker();
    //          newObj.AllegianceChannel = binaryReader.ReadUInt32();
    //          newObj.GeneralChannel = binaryReader.ReadUInt32();
    //          newObj.TradeChannel = binaryReader.ReadUInt32();
    //          newObj.LFGChannel = binaryReader.ReadUInt32();
    //          newObj.RoleplayChannel = binaryReader.ReadUInt32();
    //          newObj.Olthoi = binaryReader.ReadUInt32();
    //          newObj.Society = binaryReader.ReadUInt32();
    //          newObj.SocietyCelHan = binaryReader.ReadUInt32();
    //          newObj.SocietyEldWeb = binaryReader.ReadUInt32();
    //          newObj.SocietyRadBlo = binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //          rootNode.Nodes.Add("AllegianceChannel = " + AllegianceChannel);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4});
    //          rootNode.Nodes.Add("GeneralChannel = " + GeneralChannel);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("TradeChannel = " + TradeChannel);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("LFGChannel = " + LFGChannel);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("RoleplayChannel = " + RoleplayChannel);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("Olthoi = " + Olthoi);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("Society = " + Society);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("SocietyCelHan = " + SocietyCelHan);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("SocietyEldWeb = " + SocietyEldWeb);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("SocietyRadBlo = " + SocietyRadBlo);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class WeenieError : Message {
    //      public WERROR etype;

    //      public static WeenieError read(BinaryReader binaryReader) {
    //          WeenieError newObj = new WeenieError();
    //          newObj.etype = (WERROR)binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView) {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //          rootNode.Nodes.Add("etype = " + etype);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class WeenieErrorWithString : Message {
    //      public WERROR etype;
    //      public PStringChar user_data;

    //      public static WeenieErrorWithString read(BinaryReader binaryReader) {
    //          WeenieErrorWithString newObj = new WeenieErrorWithString();
    //          newObj.etype = (WERROR)binaryReader.ReadUInt32();
    //          newObj.user_data = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView) {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //          rootNode.Nodes.Add("etype = " + etype);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("user_data = " + user_data);
    //          ContextInfo.AddToList(new ContextInfo { Length = user_data.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class HearSpeech : Message
    //  {
    //      public PStringChar MessageText;
    //      public PStringChar SenderName;
    //      public uint SenderID;
    //      public uint ChatMessageType;

    //      public static HearSpeech read(BinaryReader binaryReader)
    //      {
    //          var newObj = new HearSpeech();
    //          newObj.MessageText = PStringChar.read(binaryReader);
    //          newObj.SenderName = PStringChar.read(binaryReader);
    //          newObj.SenderID = binaryReader.ReadUInt32();
    //          newObj.ChatMessageType = binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });
    //          rootNode.Nodes.Add("MessageText = " + MessageText.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = MessageText.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("SenderName = " + SenderName.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = SenderName.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("SenderID = " + Utility.FormatHex(this.SenderID));
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("ChatMessageType = " + (eChatTypes)ChatMessageType);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class HearRangedSpeech : Message
    //  {
    //      public PStringChar MessageText;
    //      public PStringChar SenderName;
    //      public uint SenderID;
    //      public float Range;
    //      public uint ChatMessageType;

    //      public static HearRangedSpeech read(BinaryReader binaryReader)
    //      {
    //          var newObj = new HearRangedSpeech();
    //          newObj.MessageText = PStringChar.read(binaryReader);
    //          newObj.SenderName = PStringChar.read(binaryReader);
    //          newObj.SenderID = binaryReader.ReadUInt32();
    //          newObj.Range = binaryReader.ReadSingle();
    //          newObj.ChatMessageType = binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.Opcode });
    //          rootNode.Nodes.Add("MessageText = " + MessageText.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = MessageText.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("SenderName = " + SenderName.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = SenderName.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("SenderID = " +Utility.FormatHex(this.SenderID));
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //          rootNode.Nodes.Add("Range = " + Range);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("ChatMessageType = " + (eChatTypes)ChatMessageType);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class HearDirectSpeech : Message
    //  {
    //      public PStringChar MessageText;
    //      public PStringChar SenderName;
    //      public uint SenderID;
    //      public uint TargetID;
    //      public uint ChatMessageType;
    //      public uint secretFlags;  // Unknown use

    //      public static HearDirectSpeech read(BinaryReader binaryReader)
    //      {
    //          var newObj = new HearDirectSpeech();
    //          newObj.MessageText = PStringChar.read(binaryReader);
    //          newObj.SenderName = PStringChar.read(binaryReader);
    //          newObj.SenderID = binaryReader.ReadUInt32();
    //          newObj.TargetID = binaryReader.ReadUInt32();
    //          newObj.ChatMessageType = binaryReader.ReadUInt32();
    //          newObj.secretFlags = binaryReader.ReadUInt32();
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //          rootNode.Nodes.Add("MessageText = " + MessageText.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = MessageText.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("SenderName = " + SenderName.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = SenderName.Length, DataType = DataType.Serialized_AsciiString });
    //          rootNode.Nodes.Add("SenderID = " + Utility.FormatHex(this.SenderID));
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //          rootNode.Nodes.Add("TargetID = " + Utility.FormatHex(this.TargetID));
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ObjectID });
    //          rootNode.Nodes.Add("ChatMessageType = " + (eChatTypes)ChatMessageType);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          rootNode.Nodes.Add("secretFlags = " + secretFlags);
    //          ContextInfo.AddToList(new ContextInfo { Length = 4 });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    //  public class TransientString : Message
    //  {
    //      public PStringChar StringMessage;

    //      public static TransientString read(BinaryReader binaryReader)
    //      {
    //          var newObj = new TransientString();
    //          newObj.StringMessage = PStringChar.read(binaryReader);
    //          return newObj;
    //      }

    //      public override void contributeToTreeView(TreeView treeView)
    //      {
    //          TreeNode rootNode = new TreeNode(this.GetType().Name);
    //          rootNode.Expand();
    //          ContextInfo.AddToList(new ContextInfo { DataType = DataType.ServerToClientHeader });
    //          rootNode.Nodes.Add("StringMessage = " + StringMessage.m_buffer);
    //          ContextInfo.AddToList(new ContextInfo { Length = StringMessage.Length, DataType = DataType.Serialized_AsciiString });
    //          treeView.Nodes.Add(rootNode);
    //      }
    //  }

    public class TextBoxString
    {
        public PStringChar MessageText;
        public uint ChatMessageType;

        public static TextBoxString read(BinaryReader binaryReader)
        {
            var newObj = new TextBoxString();
            newObj.MessageText = PStringChar.read(binaryReader);
            newObj.ChatMessageType = binaryReader.ReadUInt32();
            return newObj;
        }
    }
}
