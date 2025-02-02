# UnityCustomPropery

UnityCustomPropery は、Unity のインスペクター表示をより柔軟にカスタマイズするためのカスタム Property Drawer やエディタ拡張をまとめたライブラリです。
これらのツールを利用することで、プロパティの表示や編集方法を簡単に変更でき、開発効率や見た目の向上に寄与します。

# 対応Attributes　

## GetComponent

子供からコンポーネントを選んでセットできる

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/GetComponent.png" width="700">

```csharp
[GetComponent]
public MeshFilter _meshFilter;
```


## SearchableEnum

検索できるEnumフィールドを表示する

<img src="https://cdn-ak.f.st-hatena.com/images/fotolife/h/hacchi_man/20200329/20200329004126.png" width="300">

```csharp
[SearchableEnum]
public MyEnum _searchableEnum;
```

## Preview

Previewが表示される型であれば表示することができる

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/Preview.png" width="700">

```csharp
[Preview]
public Texture _texture;
```

## Color

既存のカラーフィールドにRGBAのスライダーとカラーコード表示機能を追加

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/Color.png" width="700">

## Text

TextMeshProUGUI　の　Textを表示編集ができる

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/Text.png" width="700">

## ValidateNotNull

あたいがセットされてないならエラーを表示する

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/ValidateNotNull.png" width="700">


```csharp
[ValidateNotNull]
public MeshFilter _meshFilter;
```

## ObjectRestrict

オブジェクトに制限をかける

制限は以下の3種類です

```csharp
/// <summary>
/// 子供のオブジェクトのみ
/// </summary>
[SerializeField, ObjectRestrict(TargetType.ChildOnly)]
private GameObject _obj;
 
/// <summary>
/// Hierarchyのオブジェクトのみ
/// </summary>
[SerializeField, ObjectRestrict(TargetType.InHierarchy)]
private GameObject _obj;
 
/// <summary>
/// Project リソースのみ
/// </summary>
[SerializeField, ObjectRestrict(TargetType.InProject)]
private GameObject _obj;
```

# ライセンス
本プロジェクトは MIT License の下でライセンスされています。
詳細については、LICENSE ファイルをご覧ください。


# UnityCustomPropery
UnityCustomPropery は、Unity のインスペクター表示をより柔軟にカスタマイズするためのカスタム Property Drawer やエディタ拡張をまとめたライブラリです。
これらのツールを利用することで、プロパティの表示や編集方法を簡単に変更でき、開発効率や見た目の向上に寄与します。

# 対応 Attributes
## GetComponent
子オブジェクトから目的のコンポーネントを選択してセットできるようにします。

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/GetComponent.png" width="300">
```csharp
[GetComponent]
public MeshFilter _meshFilter;
```

## SearchableEnum
Enum フィールドを検索可能なドロップダウンとして表示します。

<img src="https://cdn-ak.f.st-hatena.com/images/fotolife/h/hacchi_man/20200329/20200329004126.png" width="300">
```csharp
[SearchableEnum]
public MyEnum _searchableEnum;
```

## Preview
対応する型であれば、フィールドのプレビュー画像をインスペクター上に表示します。

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/Preview.png" width="300">
```csharp
[Preview]
public Texture _texture;
```

## Color
既存のカラーフィールドに対して、RGBA のスライダーとカラーコード表示機能を追加します。

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/Color.png" width="300" alt="Color">


## Text
TextMeshProUGUI のテキストコンテンツをインスペクター上で直接編集できるようにします。

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/Text.png" width="300" alt="Text">

```csharp
[Text]
public TextMeshProUGUI _textMeshPro;
```

## ValidateNotNull
値が未設定 (null) の場合、エラーを表示します。

<img src="https://github.com/yayorozu/ImageUploader/blob/master/CustomAttr/ValidateNotNull.png" width="300" alt="ValidateNotNull">

```csharp
[ValidateNotNull]
public MeshFilter _meshFilter;
```

## ObjectRestrict
対象オブジェクトの参照先を制限します。制限は以下の 3 種類です。

#### ChildOnly
子オブジェクトのみを許可

#### InHierarchy
Hierarchy 上のオブジェクトのみを許可

#### InProject
Project 内のリソースのみを許可

```csharp
/// <summary>
/// 子オブジェクトのみを許可
/// </summary>
[SerializeField, ObjectRestrict(TargetType.ChildOnly)]
private GameObject _obj;

/// <summary>
/// Hierarchy 上のオブジェクトのみを許可
/// </summary>
[SerializeField, ObjectRestrict(TargetType.InHierarchy)]
private GameObject _obj;

/// <summary>
/// Project 内のリソースのみを許可
/// </summary>
[SerializeField, ObjectRestrict(TargetType.InProject)]
private GameObject _obj;
```

# ライセンス
本プロジェクトは MIT License の下でライセンスされています。
詳細については、LICENSE ファイルをご覧ください。
