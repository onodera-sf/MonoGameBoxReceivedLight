using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BoxReceivedLight
{
	/// <summary>
	/// ゲームメインクラス
	/// </summary>
	public class Game1 : Game
	{
    /// <summary>
    /// グラフィックデバイス管理クラス
    /// </summary>
    private readonly GraphicsDeviceManager _graphics = null;

    /// <summary>
    /// スプライトのバッチ化クラス
    /// </summary>
    private SpriteBatch _spriteBatch = null;

    /// <summary>
    /// 基本エフェクト
    /// </summary>
    private BasicEffect _basicEffect = null;

    /// <summary>
    /// 頂点バッファ
    /// </summary>
    private VertexBuffer _vertexBuffer = null;

    /// <summary>
    /// インデックスバッファ
    /// </summary>
    private IndexBuffer _indexBuffer = null;

    /// <summary>
    /// インデックスバッファの各頂点番号配列
    /// </summary>
    private static readonly short[] _vertexIndices = new short[] {
            2, 0, 1, 1, 3, 2, 4, 0, 2, 2, 6, 4, 5, 1, 0, 0, 4, 5,
            7, 3, 1, 1, 5, 7, 6, 2, 3, 3, 7, 6, 4, 6, 7, 7, 5, 4 };


    /// <summary>
    /// GameMain コンストラクタ
    /// </summary>
    public Game1()
    {
      // グラフィックデバイス管理クラスの作成
      _graphics = new GraphicsDeviceManager(this);

      // ゲームコンテンツのルートディレクトリを設定
      Content.RootDirectory = "Content";

      // マウスカーソルを表示
      IsMouseVisible = true;
    }

    /// <summary>
    /// ゲームが始まる前の初期化処理を行うメソッド
    /// グラフィック以外のデータの読み込み、コンポーネントの初期化を行う
    /// </summary>
    protected override void Initialize()
    {
      // TODO: ここに初期化ロジックを書いてください

      // コンポーネントの初期化などを行います
      base.Initialize();
    }

    /// <summary>
    /// ゲームが始まるときに一回だけ呼ばれ
    /// すべてのゲームコンテンツを読み込みます
    /// </summary>
    protected override void LoadContent()
    {
      // テクスチャーを描画するためのスプライトバッチクラスを作成します
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      // エフェクトを作成
      _basicEffect = new BasicEffect(GraphicsDevice);

      // エフェクトでライトを有効にする
      _basicEffect.LightingEnabled = true;

      // デフォルトのライトの設定を使用する
      _basicEffect.EnableDefaultLighting();

      // スペキュラーを無効
      _basicEffect.SpecularColor = Vector3.Zero;

      // ２番目と３番目のライトを無効
      _basicEffect.DirectionalLight1.Enabled = false;
      _basicEffect.DirectionalLight2.Enabled = false;


      // ビューマトリックスをあらかじめ設定 ((6, 6, 12) から原点を見る)
      _basicEffect.View = Matrix.CreateLookAt(
              new Vector3(6.0f, 6.0f, 12.0f),
              Vector3.Zero,
              Vector3.Up
          );

      // プロジェクションマトリックスをあらかじめ設定
      _basicEffect.Projection = Matrix.CreatePerspectiveFieldOfView(
              MathHelper.ToRadians(45.0f),
              (float)GraphicsDevice.Viewport.Width /
                  (float)GraphicsDevice.Viewport.Height,
              1.0f,
              100.0f
          );

      // 頂点の数
      int vertexCount = 8;

      // 頂点バッファ作成
      _vertexBuffer = new VertexBuffer(GraphicsDevice,
          typeof(VertexPositionNormalTexture), vertexCount, BufferUsage.None);

      // 頂点データを作成する
      VertexPositionNormalTexture[] vertives = new VertexPositionNormalTexture[vertexCount];

      vertives[0] = new VertexPositionNormalTexture(
          new Vector3(-2.0f, 2.0f, -2.0f),
          Vector3.Normalize(new Vector3(-1.0f, 1.0f, -1.0f)),
          Vector2.Zero);
      vertives[1] = new VertexPositionNormalTexture(
          new Vector3(2.0f, 2.0f, -2.0f),
          Vector3.Normalize(new Vector3(1.0f, 1.0f, -1.0f)),
          Vector2.Zero);
      vertives[2] = new VertexPositionNormalTexture(
          new Vector3(-2.0f, 2.0f, 2.0f),
          Vector3.Normalize(new Vector3(-1.0f, 1.0f, 1.0f)),
          Vector2.Zero);
      vertives[3] = new VertexPositionNormalTexture(
          new Vector3(2.0f, 2.0f, 2.0f),
          Vector3.Normalize(new Vector3(1.0f, 1.0f, 1.0f)),
          Vector2.Zero);
      vertives[4] = new VertexPositionNormalTexture(
          new Vector3(-2.0f, -2.0f, -2.0f),
          Vector3.Normalize(new Vector3(-1.0f, -1.0f, -1.0f)),
          Vector2.Zero);
      vertives[5] = new VertexPositionNormalTexture(
          new Vector3(2.0f, -2.0f, -2.0f),
          Vector3.Normalize(new Vector3(1.0f, -1.0f, -1.0f)),
          Vector2.Zero);
      vertives[6] = new VertexPositionNormalTexture(
          new Vector3(-2.0f, -2.0f, 2.0f),
          Vector3.Normalize(new Vector3(-1.0f, -1.0f, 1.0f)),
          Vector2.Zero);
      vertives[7] = new VertexPositionNormalTexture(
          new Vector3(2.0f, -2.0f, 2.0f),
          Vector3.Normalize(new Vector3(1.0f, -1.0f, 1.0f)),
          Vector2.Zero);

      // 頂点データを頂点バッファに書き込む
      _vertexBuffer.SetData(vertives);

      // インデックスバッファを作成
      _indexBuffer = new IndexBuffer(GraphicsDevice,
          IndexElementSize.SixteenBits, 3 * 12, BufferUsage.None);

      // 頂点インデックスを書き込む
      _indexBuffer.SetData(_vertexIndices);
    }

    /// <summary>
    /// ゲームが終了するときに一回だけ呼ばれ
    /// すべてのゲームコンテンツをアンロードします
    /// </summary>
    protected override void UnloadContent()
    {
      // TODO: ContentManager で管理されていないコンテンツを
      //       ここでアンロードしてください
    }

    /// <summary>
    /// 描画以外のデータ更新等の処理を行うメソッド
    /// 主に入力処理、衝突判定などの物理計算、オーディオの再生など
    /// </summary>
    /// <param name="gameTime">このメソッドが呼ばれたときのゲーム時間</param>
    protected override void Update(GameTime gameTime)
    {
      // ゲームパッドの Back ボタン、またはキーボードの Esc キーを押したときにゲームを終了させます
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      // TODO: ここに更新処理を記述してください

      // 登録された GameComponent を更新する
      base.Update(gameTime);
    }

    /// <summary>
    /// 描画処理を行うメソッド
    /// </summary>
    /// <param name="gameTime">このメソッドが呼ばれたときのゲーム時間</param>
    protected override void Draw(GameTime gameTime)
    {
      // 画面を指定した色でクリアします
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // 描画に使用する頂点バッファをセット
      GraphicsDevice.SetVertexBuffer(_vertexBuffer);

      // インデックスバッファをセット
      GraphicsDevice.Indices = _indexBuffer;

      // パスの数だけ繰り替えし描画
      foreach (EffectPass pass in _basicEffect.CurrentTechnique.Passes)
      {
        // パスの開始
        pass.Apply();

        // ボックスを描画する
        GraphicsDevice.DrawIndexedPrimitives(
            PrimitiveType.TriangleList,
            0,
            0,
            12
        );
      }

      // 登録された DrawableGameComponent を描画する
      base.Draw(gameTime);
    }
  }
}
